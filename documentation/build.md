# Build des images docker pour le backend et le frontend

## Prérequis

- [Docker](https://docs.docker.com/get-docker/)

## Installation

Cloner le projet

```bash
git clone git@github.com:IUT-Blagnac/sae-5-1-01-phase4-c-react.git
cd sae-5-1-01-phase4-c-react
```

## Build des images

Build du backend
```bash
docker build -t sae-5-1-01-phase4-c-react_api-backend ./backend
```

Cette commande permet de build l'image du backend avec pour tag sae-5-1-01-phase4-c-react_api-backend.

Build du frontend
```bash
docker build -t sae-5-1-01-phase4-c-react_api-frontend ./frontend
```

Cette commande permet de build l'image du frontend avec pour tag sae-5-1-01-phase4-c-react_api-frontend.

## Lancement des images

### Prérequis

Lancer la base de données postgresql

```bash
docker run --name postgres -e POSTGRES_PASSWORD=postgres -e POSTGRES_USER=postgres -e POSTGRES_DB=postgres -p 5432:5432 -d postgres
```

Cette commande permet de lancer une base de données postgresql avec les identifiants par défaut sur le port 5432.
- POSTGRES_PASSWORD: postgres
- POSTGRES_USER: postgres
- POSTGRES_DB: postgres

### Lancement du backend

```bash
docker run --name api-backend -p 8080:80 -e ConnectionStrings__DatabaseConnection="Server=host.docker.internal;Port=5432;Database=postgres;Username=postgres;Password=postgres" -d sae-5-1-01-phase4-c-react_api-backend
```

Cette commande permet de lancer le backend avec les paramètres par défaut sur le port 8080.
- ConnectionStrings__DatabaseConnection: Server=host.docker.internal;Port=5432;Database=postgres;Username=postgres;Password=postgres

Le swagger du backend est accessible à l'adresse http://localhost:8080/swagger/index.html

### Lancement du frontend

```bash
docker run --name api-frontend -p 80:80 -d sae-5-1-01-phase4-c-react_api-frontend
```

Cette commande permet de lancer le frontend avec les paramètres par défaut sur le port 80.

Le frontend est accessible à l'adresse http://localhost:80