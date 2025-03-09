# 📚 Gestion d'une Bibliothèque en C# avec Sérialisation et Cryptage

## 📝 Description
Ce projet est une **application console** en **C#** permettant de gérer une bibliothèque de livres et d'utilisateurs. Il inclut :
- **Sérialisation des données** (binaire et XML)
- **Cryptage des fichiers** pour la protection des informations
- **Gestion des emprunts et des retours de livres**
- **Organisation des livres par catégories**
- **Authentification avec mot de passe pour sécuriser l'accès**

## 📌 Fonctionnalités
✅ **Ajout et suppression de livres, utilisateurs et catégories**  
✅ **Consultation des livres et des utilisateurs**  
✅ **Sérialisation des données en binaire et XML**  
✅ **Cryptage et décryptage des fichiers via `CryptoStream`**  
✅ **Authentification par mot de passe (3 essais avant suppression des fichiers)**  
✅ **Gestion des erreurs pour éviter les corruptions de fichiers**   


## 🛠 Technologies utilisées
- **Langage :** C# (.NET 6+)
- **Sérialisation :** `BinaryFormatter` (binaire) et `XmlSerializer` (XML)
- **Cryptage :** `CryptoStream` (AES)
- **Gestion des fichiers :** `System.IO`
- **Architecture :** Séparation en plusieurs projets (Console, Data, Serialization)


