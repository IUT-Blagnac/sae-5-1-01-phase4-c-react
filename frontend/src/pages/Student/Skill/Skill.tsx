import {
  Alert,
  Box,
  Button,
  Card,
  FormControl,
  Slider,
  Typography,
} from "@mui/joy";
import { Status } from "../../../assets/enums/Status.enum";
import BlankPage from "../../templates/BlankPage";
import React, { useEffect, useState } from "react";
import {
  KeyboardArrowRight,
  PlaylistAddCheckCircleRounded,
} from "@mui/icons-material";
import Loading from "../../../components/Loading";
import API_URL from "../../../env";

interface SkillCharacter {
  id: string;
  name: string;
  confidence_level: number;
}

export default function Skill() {
  const [loading, setLoading] = useState(true);
  const [skillCharacters, setSkillCharacters] = useState<SkillCharacter[]>([]);
  const [success, setSuccess] = useState(false);

  const areTheSame = (a: SkillCharacter, b: { id: string; name: string }) => {
    return a.id === b.id && a.name === b.name;
  };

  useEffect(() => {
    const userId = localStorage.getItem("userid");
    // Fetch all the skills of the user
    fetch(API_URL + `/api/Character/user/${userId}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    })
      .then((res) => {
        if (res.status === 200) {
          return res.json();
        } else {
          throw new Error("Erreur lors de la récupération des compétences");
        }
      })
      .then((allSkillsCharachters: SkillCharacter[]) => {
        // Fetch all the skills
        fetch(API_URL + `/api/Skill`, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        })
          .then((res) => {
            if (res.status === 200) {
              return res.json();
            } else {
              throw new Error("Erreur lors de la récupération des compétences");
            }
          })
          .then((data: { id: string; name: string }[]) => {
            const skillCharacters_: SkillCharacter[] = [];

            for (const skill of data) {
              let found: SkillCharacter | undefined = undefined;
              for (const skillCharacter__ of allSkillsCharachters) {
                if (areTheSame(skillCharacter__, skill)) {
                  found = skillCharacter__;
                  break;
                }
              }
              if (!found) {
                skillCharacters_.push({
                  id: skill.id,
                  name: skill.name,
                  confidence_level: 0,
                });
              } else {
                skillCharacters_.push({
                  id: skill.id,
                  name: skill.name,
                  confidence_level: found.confidence_level,
                });
              }
            }

            setSkillCharacters(skillCharacters_);
            setLoading(false);
          })
          .catch((err) => {
            console.error(err);
          });
      })
      .catch((err) => {
        console.error(err);
      });
  }, []);

  if (loading) return <Loading />;

  const handleSkillChange = (skillName: string, value: number) => {
    const newSkillCharacters = [...skillCharacters];
    for (const skillCharacter of newSkillCharacters) {
      if (skillCharacter.name === skillName) {
        skillCharacter.confidence_level = value;
      }
    }
    setSkillCharacters(newSkillCharacters);
  };

  const handleSubmit = async () => {
    const formatedSkillCharacters = [];
    // Copy the array but replace the property id by id_skill
    for (const skillCharacter of skillCharacters) {
      formatedSkillCharacters.push({
        id_skill: skillCharacter.id,
        confidence_level: skillCharacter.confidence_level,
      });
    }

    const body = {
      name: "NULL",
      id_sae: "c5710c89-1b52-473b-886e-722f97ff713a",
      skills: formatedSkillCharacters,
    };

    fetch(API_URL + "/api/Character", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
      body: JSON.stringify(body),
    })
      .then((res) => {
        if (res.status === 201) {
          setSuccess(true);
          return res.json();
        } else {
          throw new Error("Erreur lors de la création du personnage");
        }
      })
      .catch((err) => {
        console.error(err);
      });
  };

  return (
    <BlankPage
      pageTitle="Mes compétences"
      role={Status.STUDENT}
      maxContentWidth="800px"
    >
      <Card>
        <Typography level="body-sm">Renseigner mes compétences</Typography>
        {success && (
          <Box
            sx={{
              display: "flex",
              flexDirection: "column",
              gap: 2,
              width: "100%",
            }}
          >
            <Alert
              variant="soft"
              color="success"
              startDecorator={<PlaylistAddCheckCircleRounded />}
              endDecorator={
                <Button
                  size="sm"
                  variant="solid"
                  color="success"
                  onClick={() => setSuccess(false)}
                >
                  Close
                </Button>
              }
            >
              Vos compétences ont bien été enregistrées !
            </Alert>
          </Box>
        )}
        <Button
          endDecorator={<KeyboardArrowRight />}
          color="success"
          sx={{
            maxWidth: "50%",
            marginLeft: "auto",
            marginRight: "auto",
          }}
          onClick={() => {
            handleSubmit();
          }}
        >
          Sauvegarder
        </Button>
        <form>
          {skillCharacters.map((skill) => (
            <div key={skill.id} style={{ marginBottom: "16px" }}>
              <Typography>{skill.name}</Typography>
              <FormControl>
                <Typography level="body-sm">
                  Niveau {skill.confidence_level}
                </Typography>
              </FormControl>
              <div style={{ display: "flex", alignItems: "center" }}>
                <Slider
                  value={skill.confidence_level}
                  min={0}
                  max={10}
                  step={1}
                  marks
                  style={{ marginTop: "6px", flexGrow: 1 }}
                  onChange={(e, value) =>
                    handleSkillChange(skill.name, value as number)
                  }
                />
              </div>
            </div>
          ))}
        </form>
      </Card>
    </BlankPage>
  );
}
