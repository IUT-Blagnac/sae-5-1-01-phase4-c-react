# ProGest

## Content

- [Release](#-release)
- [Fonctionnalités](#-fonctionnalités)
- [Équipe](#-équipe)
- [Contexte général](#-contexte-général)
- [Déploiement](#-installation-et-déploiement)
- [Documentation](#-documentation)

## ✅ Release

Site internet : [ProGest](https://sae.homelab.mrobert.fr/)

URL du site internet : https://sae.homelab.mrobert.fr/

Projet :
https://github.com/orgs/IUT-Blagnac/projects/160
https://github.com/orgs/IUT-Blagnac/projects/167

## 🔎 Fonctionnalités

Découvrez les fonctionnalités riches et adaptées de ProGest, conçues pour répondre aux besoins spécifiques de chaque type d'utilisateur. Explorez en détail toutes les capacités offertes par notre plateforme en consultant la [Documentation Utilisateur](./documentation/doc-utilisateur.md) complète.

### 👤​ Utilisateur commun

- Page de connexion

### ​💻​ Administrateur

- Dashboard
- Manage SAE
- Créer une SAE
- Importer des utilisateurs
- Générer des teams automatiquements

### 🖋️​ Étudiant

- Fiche de compétences
- Consulter SAE

### 🧑‍🏫​ Professeur

- Consulter SAE professeur
- Noter SAE

## 👥 Équipe

Projet est réalisé par:

- [Eric PHILIPPE](https://github.com/Eric-Philippe) (Frontend)
- [Thomas TESTA](https://github.com/SkyFriz) (Frontend)
- [Marco VALLE](https://github.com/Stemon8) (Backend)
- [Loan GAYRARD](https://github.com/Sonixray) (SCRUM Master, Backend)
- [Matthieu ROBERT](https://github.com/matthieurobert) (Backend, DevOps)
- [Hugo CASTELL](https://github.com/Hugo-CASTELL) (Backend)

## 📕 Contexte général

L'IUT de Blagnac évalue ses étudiants en organisant des projets. Chaque projet est unique et a besoin d'une organisation complexe en fonction des compétences à évaluer, des professeurs et des élèves. Pour organiser un projet et avoir le suivi du projet chaque professeur à ça méthode, ses outils, qu'il a cherché avant de la partager aux élèves. Un élève doit donc s'adapter aux méthodes du professeur pour commencer à trvailler.

Dans ce contexte ProGest permet de faciliter l'organisation d'un projet en permmettant d'organiser la gestion de groupe d'élève et de suivre la progression du projet en tant que professeur.

## 🐋 Installation et déploiement

### Prérequis

- [Docker](https://docs.docker.com/get-docker/)

### Installation

1. Cloner le projet

```bash
git clone git@github.com:IUT-Blagnac/sae-5-1-01-phase4-c-react.git
cd sae-5-1-01-phase4-c-react
```

2. Lancer le docker-compose

```bash
docker-compose up -d --build
```

3. Accéder au site

Ouvre un navigateur et accède à l'adresse suivante : http://localhost:80  
Pour accéder à la documentation de l'api : http://localhost:8080/swagger/index.html

Plus d'inforamations sur le build des images docker dans la [Documentation de build](./documentation/build.md)
Plus d'informations sur l'installation et le déploiement dans la [Documentation Technique](./documentation/doc-technique-back.md)

## 📚 Documentation

|                               Documentation                               |
| :-----------------------------------------------------------------------: |
|                       [Sources backend](./backend)                        |
|                      [Sources frontend](./frontend)                       |
|      [Documentation Utilisateur](./documentation/doc-utilisateur.md)      |
| [Documentation Technique Frontend](./documentation/doc-technique-front.md) |
| [Documentation Technique Backend](./documentation/doc-technique-back.md) |
| [Documentation de build](./documentation/build.md)                        |
