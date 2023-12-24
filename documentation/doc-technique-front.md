# Documentation Frontend

## Introduction

Ce présent document a pour but de présenter les fonctionnalités de l'application et de guider l'utilisateur dans son utilisation.

## Sommaire

- [Introduction](#introduction)
- [Sommaire](#sommaire)
- [Technologies utilisés](#technologies-utilisés)
- [Principes de développement](#principes-de-développement)
  - [Composants](#composants)
    - [Sidebar](#sidebar)
    - [BlankPage](#blankpage)
  - [Pages](#pages)
  - [Gestion des données utilisateur en mémoire](#gestion-des-données-utilisateur-en-mémoire)
  - [Gestion du style](#gestion-du-style)
- [Contact](#contact)

## Technologies utilisés

| Nom        | Version | Description        |
| ---------- | ------- | ------------------ |
| React      | 18.2.0  | Framework          |
| Node       | 20.01.0 | Runtime            |
| Typescript | 4.9.5   | Langage            |
| MuiJoy     | \*      | Librairie de style |

## Principes de développement

### Composants

#### Sidebar

`Sidebar.tsx`

La sidebar est le composant qui permet de naviguer dans l'application. Elle est entièrement dynamique et s'adapte en fonction des droits de l'utilisateur connecté.
Elle est réutilisable partout dans l'application.

```tsx
<Sidebar />
```

#### BlankPage

`BlankPage.tsx`

Ce composant est le composant principal permettant la centralisation des composants communs à toutes les pages. Il permet d'y intégrer la sidebar, les middlewares de connexion, ceux de vérification des droits.

#### API Service

L'entièreté des appels API sont centralisés dans des fichiers pour chaque entité. Cela permet d'éviter des codes de la sorte :

```tsx
fetch("...").then((res) => {
  res.json().then((data) => {
    fetch("...").then((res) => {
      res.json().then((data) => {
        fetch("...").then((res) => {
          res.json().then((data) => {
            // Do something
          });
        });
      });
    });
  });
});
```

Avec les API Services, il est possible de faire des appels API de la sorte :

```tsx
useEffect(() => {
  const fetchData = async () => {
    const data = await GroupService.getGroupById("1234567891011");
    setStudent(data);
  };
  fetchData().then(() => setLoading(false));
}, []);
```

### Pages

Chaque page est rangée dans un dossier ./pages/ qui lui même se décompose en trois types de sous-dossiers majeurs :

- Admin : contient les pages accessibles uniquement par les administrateurs
- Student : contient les pages accessibles uniquement par les étudiants
- Common : contient les pages accessibles par tous les utilisateurs

### Gestion des données utilisateur en mémoire

L'application ne dépends pas de cookies pour stocker les données utilisateur. En effet, les données sont stockées en mémoire dans le contexte de l'application. Cela permet de ne pas avoir à faire de requêtes à chaque changement de page pour récupérer les données de l'utilisateur.

```tsx
localStorage.setItem("userid", "1234567891011");
```

Pour autant, changer les valeurs du localStorage depuis la console navigateur ne permettra pas à un utilisateur de s'attribuer des droits. Un middleware est appliqué à chacune des pages pour vérifier les droits de l'utilisateur.

### Gestion du style

L'entièreté de l'application est stylée avec la librairie MuiJoy. Cette librairie est une sous-variation de Material UI, elle permet de styliser l'application avec un thème personnalisé.

## Contact

Pour toute question à propos du front, évolution, ou autre, vous pouvez contacter :
<ericphlpp@gmail.com> - **Eric PHILIPPE**
