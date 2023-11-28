import { Card, FormControl, Slider, Typography } from "@mui/joy";
import { Status } from "../../../assets/enums/Status.enum";
import BlankPage from "../../templates/BlankPage";
import React from "react";

const skills = [
  "Programmation",
  "Développement web",
  "Base de données",
  "Algorithmie",
  "Conception logicielle",
  "Réseaux informatiques",
  "Sécurité informatique",
  "Intelligence artificielle",
  "Analyse de données",
  "Interface utilisateur",
];

export default function Skill() {
  const [skillValues, setSkillValues] = React.useState<{
    [key: string]: number;
  }>({});

  const handleSkillChange =
    (skill: string) => (event: React.ChangeEvent<{ value: unknown }>) => {
      setSkillValues({ ...skillValues, [skill]: event.target.value as number });
    };

  return (
    <BlankPage
      pageTitle="Mes compétences"
      role={Status.STUDENT}
      maxContentWidth="800px"
    >
      <Card>
        <form>
          {skills.map((skill) => (
            <div key={skill} style={{ marginBottom: "16px" }}>
              <Typography>{skill}</Typography>
              <FormControl>
                <Typography level="body-sm">
                  Niveau {skillValues[skill] || 0}
                </Typography>
              </FormControl>
              <div style={{ display: "flex", alignItems: "center" }}>
                <Slider
                  value={skillValues[skill] || 0}
                  min={0}
                  max={10}
                  step={1}
                  marks
                  onChange={(event, value) =>
                    handleSkillChange(skill)({
                      target: { value },
                    } as React.ChangeEvent<{ value: unknown }>)
                  }
                  style={{ marginTop: "6px", flexGrow: 1 }}
                />
              </div>
            </div>
          ))}
        </form>
      </Card>
    </BlankPage>
  );
}
