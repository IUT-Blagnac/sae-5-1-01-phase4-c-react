import { useEffect, useState } from "react";

import {
  Box,
  Card,
  Chip,
  Divider,
  FormControl,
  FormLabel,
  Input,
  Option,
  Select,
  Stack,
  Textarea,
  Typography,
} from "@mui/joy";

// Template for a Blank Page
import BlankPage from "../../templates/BlankPage";
import { Status } from "../../../assets/enums/Status.enum";

// Components
import NewTopic from "../../../components/CreateSAE/NewTopic";

// Models
import CreateSaeForm from "../../../models/CreateSaeForm";
import API_URL from "../../../env";
import { getFetchHeaders } from "../../../utils/Utils";
import Loading from "../../../components/Loading";

export default function CreateSAE() {
  const [inputText, setInputText] = useState<string>("Non, Implémenté");
  const [saeName, setSaeName] = useState<string>("");
  const [saeDescription, setSaeDescription] = useState<string>("");
  const [saeGroups, setSaeGroups] = useState<string[]>([]);
  const [saeSkills, setSaeSkills] = useState<string[]>(["Non", "Implémenté"]);
  const [saeMinTeamPerSubject, setSaeMinTeamPerSubject] = useState<number>(0);
  const [saeMaxTeamPerSubject, setSaeMaxTeamPerSubject] = useState<number>(0);
  const [saeMinTeamSize, setSaeMinTeamSize] = useState<number>(0);
  const [saeMaxTeamSize, setSaeMaxTeamSize] = useState<number>(0);
  const [saeTeachers, setSaeTeachers] = useState<string[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [categories, setCategories] = useState<{ id: string; name: string }[]>(
    []
  );
  const [groups, setGroups] = useState<{ id: string; name: string }[]>([]);
  const [teachers, setTeachers] = useState<{ id: string; fullName: string }[]>(
    []
  );

  useEffect(() => {
    fetch(API_URL + "/api/Group", {
      method: "GET",
      headers: getFetchHeaders(),
    }).then(async (res) => {
      if (res.status === 200) {
        const data = await res.json();
        setGroups(data);
        fetch(API_URL + "/api/User/teachers", {
          method: "GET",
          headers: getFetchHeaders(),
        }).then(async (res) => {
          if (res.status === 200) {
            const data = await res.json();
            setTeachers(data.teachers);
            fetch(API_URL + "/api/Category", {
              method: "GET",
              headers: getFetchHeaders(),
            }).then(async (res) => {
              if (res.status === 200) {
                const data = await res.json();
                setCategories(data);
                setLoading(false);
              }
            });
          }
        });
      }
    });
  }, []);

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setInputText(event.target.value);
    setSaeSkills(event.target.value.split(","));
  };

  const handleInputBlur = () => {
    const words = inputText.split(",");
    setInputText(words.join(","));
  };

  const handleChangeSaeGroup = (
    event: React.SyntheticEvent | null,
    newValue: Array<string> | null
  ) => {
    let value = newValue as string[];
    setSaeGroups(value);
  };

  const handleChangeSaeTeacher = (
    event: React.SyntheticEvent | null,
    newValue: Array<string> | null
  ) => {
    let value = newValue as string[];
    setSaeTeachers(value);
  };

  const handleSubmitWithoutTopic = (): CreateSaeForm => {
    let form: CreateSaeForm = {
      name: saeName,
      description: saeDescription,
      id_groups: saeGroups,
      min_groups_per_subject: saeMinTeamPerSubject,
      max_groups_per_subject: saeMaxTeamPerSubject,
      min_students_per_group: saeMinTeamSize,
      max_students_per_group: saeMaxTeamSize,
      id_coachs: saeTeachers,
      subjects: [],
    };

    return form;
  };

  if (loading) return <Loading />;

  return (
    <BlankPage
      role={Status.ADMIN}
      pageTitle="Créer une SAE"
      maxContentWidth="900px"
    >
      <Card>
        <Box sx={{ mb: 1 }}>
          <Typography level="title-md">Nouvelle SAE</Typography>
          <Typography level="body-sm">
            Ajouter une nouvelle SAE. <br />
            Attention, une SAE contient un ou plusieurs sujets !
          </Typography>
        </Box>
        <Divider />

        <Stack direction={{ xs: "column", md: "row" }} spacing={3} my={1}>
          <Stack spacing={2} sx={{ flexGrow: 1 }}>
            {/**  ============ */}
            {/** @STACK_DE_NOM */}
            {/**  ==================== */}
            <Stack spacing={1}>
              <FormLabel>Nom</FormLabel>
              <FormControl
                sx={{
                  display: {
                    sm: "flex-column",
                    md: "flex-row",
                  },
                  gap: 2,
                }}
              >
                <Input
                  size="sm"
                  placeholder="Insérer nom de la SAE"
                  value={saeName}
                  onChange={(e) => {
                    setSaeName(e.target.value);
                  }}
                />
              </FormControl>
            </Stack>

            {/**  ==================== */}
            {/** @STACK_DE_DESCRIPTION */}
            {/**  ==================== */}
            <Stack spacing={1}>
              <FormLabel>Description</FormLabel>
              <FormControl
                sx={{
                  display: {
                    sm: "flex-column",
                    md: "flex-row",
                  },
                  gap: 2,
                }}
              >
                <Textarea
                  minRows={4}
                  placeholder="Décrivez la SAE en quelques mots, rajoutez des liens vers des ressources externes si besoin"
                  value={saeDescription}
                  onChange={(e) => {
                    setSaeDescription(e.target.value);
                  }}
                ></Textarea>
              </FormControl>
            </Stack>

            {/**  ================ */}
            {/** @STACK_DE_GROUPES */}
            {/**  ================ */}
            <Stack>
              <FormControl>
                <FormLabel>Groupes Associés</FormLabel>
                <Select
                  multiple
                  defaultValue={["3A", "3B"]}
                  renderValue={(selected) => (
                    <Box sx={{ display: "flex", gap: "0.25rem" }}>
                      {selected.map((selectedOption) => (
                        <Chip
                          variant="soft"
                          color="primary"
                          key={selectedOption.value}
                        >
                          {selectedOption.label}
                        </Chip>
                      ))}
                    </Box>
                  )}
                  sx={{
                    minWidth: "15rem",
                  }}
                  slotProps={{
                    listbox: {
                      sx: {
                        width: "100%",
                      },
                    },
                  }}
                  value={saeGroups}
                  onChange={handleChangeSaeGroup}
                >
                  {groups.map((group) => (
                    <Option key={group.id} value={group.id}>
                      {group.name}
                    </Option>
                  ))}
                </Select>
              </FormControl>
            </Stack>

            {/**  ==================== */}
            {/** @STACK_DE_COMPETENCES */}
            {/**  ==================== */}
            <Stack>
              <FormControl>
                <FormLabel>
                  Champ Feuille de compétence (Non implémenté)
                </FormLabel>
                <Input
                  value={inputText}
                  onChange={handleInputChange}
                  onBlur={handleInputBlur}
                  placeholder="Ajouter des mots-clés"
                />
                <Box
                  sx={{
                    display: "flex",
                    gap: "0.25rem",
                    marginTop: "0.5rem",
                  }}
                >
                  {inputText !== "" ? (
                    inputText.split(",").map((word, index) => (
                      <Chip key={index} variant="outlined" color="primary">
                        {word.trim()}
                      </Chip>
                    ))
                  ) : (
                    <></>
                  )}
                </Box>
              </FormControl>
            </Stack>

            {/**  ======================= */}
            {/** @STACK_DE_ETUDIANT_BY_GP */}
            {/**  ======================= */}
            <Stack direction={{ xs: "column", md: "row" }} spacing={2}>
              <FormControl sx={{ flexGrow: 1 }}>
                <FormLabel>Min. Étudiant/Groupe</FormLabel>
                <Input
                  type="number"
                  value={saeMinTeamSize}
                  onChange={(e) => {
                    setSaeMinTeamSize(parseInt(e.target.value));
                  }}
                />
              </FormControl>
              <FormControl sx={{ flexGrow: 1 }}>
                <FormLabel>Max. Étudiant/Groupe</FormLabel>
                <Input
                  placeholder="Laisser vide pour illimité"
                  type="number"
                  value={saeMaxTeamSize}
                  onChange={(e) => {
                    setSaeMaxTeamSize(parseInt(e.target.value));
                  }}
                />
              </FormControl>
            </Stack>

            {/**  ======================== */}
            {/** @STACK_DE_GROUPE_BY_TOPIC */}
            {/**  ======================== */}
            <Stack direction={{ xs: "column", md: "row" }} spacing={2}>
              <FormControl sx={{ flexGrow: 1 }}>
                <FormLabel>Min. Groupe/Sujet</FormLabel>
                <Input
                  type="number"
                  value={saeMinTeamPerSubject}
                  onChange={(e) => {
                    setSaeMinTeamPerSubject(parseInt(e.target.value));
                  }}
                />
              </FormControl>
              <FormControl sx={{ flexGrow: 1 }}>
                <FormLabel>Max. Groupe/Sujet</FormLabel>
                <Input
                  placeholder="Laisser vide pour illimité"
                  type="number"
                  value={saeMaxTeamPerSubject}
                  onChange={(e) => {
                    setSaeMaxTeamPerSubject(parseInt(e.target.value));
                  }}
                />
              </FormControl>
            </Stack>

            {/**  ====================== */}
            {/** @STACK_COACH_LINKED_SAE */}
            {/**  ====================== */}
            <Stack>
              <FormControl>
                <FormLabel>Coachs Associés</FormLabel>
                <Select
                  multiple
                  defaultValue={["Esther Pendaries", "Jean-Michel Bruel"]}
                  renderValue={(selected) => (
                    <Box sx={{ display: "flex", gap: "0.25rem" }}>
                      {selected.map((selectedOption) => (
                        <Chip
                          variant="soft"
                          color="primary"
                          key={selectedOption.value}
                        >
                          {selectedOption.label}
                        </Chip>
                      ))}
                    </Box>
                  )}
                  sx={{
                    minWidth: "15rem",
                  }}
                  slotProps={{
                    listbox: {
                      sx: {
                        width: "100%",
                      },
                    },
                  }}
                  value={saeTeachers}
                  onChange={handleChangeSaeTeacher}
                >
                  {teachers.map((teacher) => (
                    <Option key={teacher.id} value={teacher.id}>
                      {teacher.fullName}
                    </Option>
                  ))}
                </Select>
              </FormControl>
            </Stack>
          </Stack>
        </Stack>
      </Card>
      <NewTopic submitSae={handleSubmitWithoutTopic} categories={categories} />
    </BlankPage>
  );
}
