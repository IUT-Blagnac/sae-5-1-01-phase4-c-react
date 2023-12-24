# Story Time :

## Étape 1 -

- Un administrateur créé une SAE

[Page Creation SAE]
{Formulaire}

- - Nom (Ex: Koh-Lanta)
- - Description (Ex: Faites ceci cela, lien vers Moodle: )
- - Groupe(s) associé (Ex : Selection multiple parmis les groupes existants)
- - Champ Feuille de Skil (Ex: Rentrer dans les champs, séparer pas une virgule les différents skillz ("Web, Management, DevOps"))
- - Max et min nombre d'équipe par sujet (Ex: 2 et 4)
- - Compte Coach (Teacher) associés
- - - Ajout de sujets dynamique avec juste les champs : Nom, Description
- - - Catégorie (Web, Refactor, DevOps...)

{Formulaire Technique}

```json
{
  "name": "Koh-Lanta",
  "description": "Faites ceci cela, lien vers Moodle: ",
  "groups": ["Groupe 1", "Groupe 2"],
  "skills": ["Web", "Management", "DevOps"],
  "teachers": ["Teacher 1", "Teacher 2"],
  "minTeamSize": 2,
  "maxTeamSize": 2,
  "maxTeamPerSubject": 4,
  "minTeamPerSubject": 2,
  "subjects": [
    {
      "name": "Sujet 1",
      "description": "Description du sujet 1",
      "category": ["Web", "Refactor"]
    },
    {
      "name": "Sujet 2",
      "description": "Description du sujet 2",
      "category": ["DevOps"]
    }
  ]
}
```

## Étape 2 -

- Les étudiants concernés voient sur leurs dashboard la SAE

Ils doivent remplir la fiche de compétence

{Formulaire Technique}

```json
{
  "skills": [
    {
      "name": "Web",
      "level": 3
    },
    {
      "name": "Management",
      "level": 2
    },
    {
      "name": "DevOps",
      "level": 1
    }
  ]
}
```

## Étape 3 -

- L'administrateur revient sur la dashboard, puis clique sur la dite SAE et voit les étudiants qui ont rempli la fiche de compétence
  _(On utilisera la présence ou non de groupes reliés à la SAE pour vérifier que la SAE soit à ce stade)_
- De là il peut cliquer sur "Générer les groupes" et il obtient une liste de groupes

## Étape 4 -

- L'étudiant peut alors aller sur sa dashboard, séléctionner la SAE et voir le groupe dans lequel il est
- De là, **UN ETUDIANT PAR GROUPE** peut faire un voeu de sujet (Il peut en faire plusieurs)
  _(Si un étudiant du même groupe fait un voeu, on regarde si le groupe a déjà ses voeux de setup, auquel cas on le jette)_

## Étape 5 -

- L'administrateur revient sur la dashboard, puis clique sur la dite SAE et voit combien de voeux ont été fait pour combien de groupe
- De là il peut cliquer sur "Attribuer les sujets" et il obtient une association groupe/sujet

## Étape 6 -

- L'étudiant peut alors aller sur sa dashboard, séléctionner la SAE et voir maintenant en plus le sujet qui lui a été attribué
- Il peut maintenant cliquer sur le sujet et voir les informations associées, et remplir les champs suivants

{Formulaire Technique}

```json
{
  "gitUrl": "URL",
  "A_COMPLETER": "A_COMPLETER"
}
```

## Étape 7 -

- L'administrateur revient sur la dashboard, et peut activer le bouton "Activer les défis"
- De là les étudiants peuvent voir qu'ils peuvent donner un défi à un autre groupe sur le même sujet qu'eux
- Ils peuvent alors cliquer sur le groupe et donner un défi (C'est super brouillon, et complexe à mettre en place)
- A chaque fois qu'un admin clique sur "Activer les défis", tous les précédents sont détruits

## Étape 7 BIS

- L'administrateur revient sur la dashboard, et peut activer le bouton "Autoriser des nouveaux membres d'équipes"
- De là, une seconde salve d'étudiants ayant rempli la fiche de compétence peut rejoindre les groupes qui ne sont pas encore inscrits

## Étape 8 -

- Les coachs et admin peuvent voir les sujets et les étudiants associés et donner une note (on fait simple pour le moment)
- Les coachs et clients seront sous le même rôle (Teacher) pour le moment par simplicité et temps (Eric - J'ai pas le temps de développer 4 interfaces différentes, et les permissions des deux sont les mêmes)
