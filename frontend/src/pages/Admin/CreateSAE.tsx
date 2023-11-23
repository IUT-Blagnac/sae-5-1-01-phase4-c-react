import { useState } from "react";

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
import BlankPage from "../templates/BlankPage";
import { Status } from "../../assets/enums/Status.enum";

// Components
import NewTopic from "../../components/CreateSAE/NewTopic";

// Models
import CreateSaeForm from "../../models/CreateSaeForm";

export default function CreateSAE() {
  const [inputText, setInputText] = useState<string>("");
  const [saeName, setSaeName] = useState<string>("");
  const [saeDescription, setSaeDescription] = useState<string>("");
  const [saeGroups, setSaeGroups] = useState<string[]>([]);
  const [saeSkills, setSaeSkills] = useState<string[]>([]);
  const [saeMinTeamPerSubject, setSaeMinTeamPerSubject] = useState<number>(0);
  const [saeMaxTeamPerSubject, setSaeMaxTeamPerSubject] = useState<number>(0);
  const [saeMinTeamSize, setSaeMinTeamSize] = useState<number>(0);
  const [saeMaxTeamSize, setSaeMaxTeamSize] = useState<number>(0);
  const [saeTeachers, setSaeTeachers] = useState<string[]>([]);

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
      groups: saeGroups,
      skills: saeSkills,
      minTeamPerSubject: saeMinTeamPerSubject,
      maxTeamPerSubject: saeMaxTeamPerSubject,
      minTeamSize: saeMinTeamSize,
      maxTeamSize: saeMaxTeamSize,
      teachers: saeTeachers,
      subjects: [],
    };

    return form;
  };

  return (
    <BlankPage
      role={localStorage.getItem("statut") as Status}
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
                  <Option value="3A">3.A</Option>
                  <Option value="3B">3.B</Option>
                  <Option value="31">3.1</Option>
                  <Option value="32">3.2</Option>

                  <Option value="21A">2.1A</Option>
                  <Option value="21B">2.1B</Option>
                  <Option value="22A">2.2A</Option>
                  <Option value="22B">2.2B</Option>
                  <Option value="23A">2.3A</Option>
                  <Option value="23B">2.3B</Option>

                  <Option value="11A">1.1A</Option>
                  <Option value="11B">1.1B</Option>
                  <Option value="12A">1.2A</Option>
                  <Option value="12B">1.2B</Option>
                  <Option value="13A">1.3A</Option>
                  <Option value="13B">1.3B</Option>
                  <Option value="14A">1.4A</Option>
                  <Option value="14B">1.4B</Option>
                </Select>
              </FormControl>
            </Stack>

            {/**  ==================== */}
            {/** @STACK_DE_COMPETENCES */}
            {/**  ==================== */}
            <Stack>
              <FormControl>
                <FormLabel>Champ Feuille de compétence</FormLabel>
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
                  {inputText != "" ? (
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
                  <Option value="EP">Esther Pendaries</Option>
                  <Option value="PSE">Pablo Seban</Option>
                  <Option value="PSO">Pascal Sotin</Option>
                  <Option value="JMB">Jean-Michel Bruel</Option>
                </Select>
              </FormControl>
            </Stack>
          </Stack>
        </Stack>
      </Card>
      <NewTopic submitSae={handleSubmitWithoutTopic} />
    </BlankPage>
  );
}
