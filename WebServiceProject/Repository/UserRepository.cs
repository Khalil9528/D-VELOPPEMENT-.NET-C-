using Microsoft.EntityFrameworkCore;
using WebServiceProject.Data;
using WebServiceProject.Intefraces;
using WebServiceProject.Interfaces;
using WebServiceProject.Models;
using Serialization;
using Microsoft.SqlServer.Server;

namespace WebServiceProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Récupérer tous les utilisateurs
        public ICollection<User> GetUsers()
        {
            return _dataContext.Users.OrderBy(u => u.Id).ToList();
        }

        // Récupérer un utilisateur par ID
        public User GetUserById(int id)
        {
            return _dataContext.Users.FirstOrDefault(u => u.Id == id);
        }
  
        // Créer un nouvel utilisateur
        public bool CreateUser(User user)
        {

            user.Password = PasswordEncryption.Encrypt(user.Password);

            _dataContext.Users.Add(user);



            // Récupérer le chemin du répertoire courant
            string currentDirectory = Directory.GetCurrentDirectory();

            // Nom du fichier à créer
            string fileName = user.Pseudo;

            // Chemin complet du fichier
            string filePath0 = Path.Combine(currentDirectory, fileName);



            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "user.bin");
            string filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "user.txt");

            // SérialZser l'utilisateur dans le fichier unique
            var Binaryserializer = new BinarySerialization();
            Binaryserializer.Serialize(user.Pseudo, filePath);
            File.WriteAllText(filePath0, user.Pseudo);
            File.AppendAllText(filePath2, user.Pseudo);
            FileEncryption.EncryptLibrary("user.txt", "user.bin");


            // Enregistrer les modifications dans la base de données
            return Save();
        }

        // Mettre à jour un utilisateur existant
        public bool UpdateUser(User user)
        {
            _dataContext.Users.Update(user);
            return Save();
        }

        // Supprimer un utilisateur
        public bool DeleteUser(User user)
        {
            _dataContext.Users.Remove(user);
            return Save();
        }

        // Enregistrer les changements dans la base de données
        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }
    }
}
