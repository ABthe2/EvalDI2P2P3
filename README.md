# 📌 Guide d'installation et d'utilisation de l'application

## ⚠️ Avant de lancer l'application

Avant de démarrer l'application, assurez-vous de **vérifier les chaînes de connexion** dans `appsettings.json` afin de vous connecter à votre propre serveur SQL.

## 🔑 Génération des clés RSA  

Une paire de clés de chiffrement **(RSA)** sera automatiquement générée lors du **premier build** de l'application.  
Les clés seront placées dans le dossier `Keys/`.

## 🚀 Première utilisation  

Lors de la **première utilisation** de l'application, exécutez les commandes suivantes :  

```sh
# 1️⃣ Restauration des dépendances  
dotnet restore  

# 2️⃣ Compilation du projet  
dotnet build  

# 3️⃣ Création de la migration initiale  
dotnet ef migrations add InitialCreate --project Eval.Repositories --startup-project Eval.API  

# 4️⃣ Mise à jour de la base de données  
dotnet ef database update --project Eval.Repositories --startup-project Eval.API
```

# ❗ En cas de problème

Si l'API ne fonctionne pas correctement, ***envoyez-moi un message***. Il se peut que mon `.gitignore` ait exclu des fichiers essentiels. 📩
