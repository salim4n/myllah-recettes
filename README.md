# Myllah-Recette

Myllah-Recette est une Progressive Web App (PWA) développée avec Blazor, connectée à un backend serverless hébergé sur Azure. Cette application vous permet de gérer et partager vos recettes préférées.

## Fonctionnalités

- Affichage de recettes avec images et instructions.
- Ajout de nouvelles recettes.
- Recherche de recettes.
- Partage de recettes avec d'autres utilisateurs.

## Technologies Utilisées

- [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) : Framework de développement d'interface utilisateur pour les applications web.
- [Azure Functions](https://azure.microsoft.com/services/functions) : Backend serverless pour la gestion des données.
- [Azure Static Web Apps](https://azure.microsoft.com/services/app-service/static) : Hébergement de l'application web.
- [Azure Blob Storage](https://azure.microsoft.com/services/storage/blobs) : Stockage des images des recettes.

## Configuration

Assurez-vous d'avoir configuré les variables d'environnement suivantes dans votre projet :

- `AZURE_STORAGE_CONNECTION_STRING` : La chaîne de connexion au compte de stockage Azure Blob pour le stockage des images.
- `AZURE_FUNCTION_ENDPOINT` : L'URL de l'endpoint Azure Functions pour l'accès au backend.

## Installation

1. Clonez ce référentiel sur votre machine locale.
2. Exécutez `dotnet build` pour compiler le projet.
3. Configurez les variables d'environnement mentionnées ci-dessus.
4. Exécutez l'application avec `dotnet run`.


