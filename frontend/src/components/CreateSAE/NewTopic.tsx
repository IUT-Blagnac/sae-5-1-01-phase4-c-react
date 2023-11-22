import { useState } from "react";
import {
  Box,
  Button,
  Card,
  CardActions,
  CardOverflow,
  Divider,
  FormControl,
  FormHelperText,
  FormLabel,
  Input,
  Select,
  Stack,
  Textarea,
  Typography,
  Option,
} from "@mui/joy";

const MAX_DESCRIPTION_LENGTH = 400;

interface NewTopicProps {
  submitSae: () => void;
}

export default function NewTopic({ submitSae }: NewTopicProps) {
  const [topics, setTopics] = useState([
    {
      id: 1,
      titleText: "",
      descriptionText: "",
      selectedCategories: ["WebDev", "Refactoring"],
    },
  ]);

  const handleAddTopic = () => {
    const newTopic = {
      id: topics.length + 1,
      titleText: "",
      descriptionText: "",
      selectedCategories: ["WebDev", "Refactoring"],
    };

    setTopics((prevTopics) => [...prevTopics, newTopic]);
  };

  const handleDeleteTopic = (idToDelete: number) => {
    setTopics((prevTopics) =>
      prevTopics.filter((topic) => topic.id !== idToDelete)
    );
  };

  return (
    <>
      {topics.map((topic) => (
        <Card key={topic.id}>
          <Box sx={{ mb: 1 }}>
            <Typography level="title-md">Ajouter un Sujet</Typography>
            <Typography level="title-sm">Sujet: {topic.titleText}</Typography>
            <Typography level="body-sm">
              Décrire les spécificités de votre sujet
            </Typography>
          </Box>
          <Divider />
          {/** @TITLE_STACK */}
          <Stack spacing={2} sx={{ my: 1 }}>
            <FormLabel>Nom</FormLabel>
            <FormControl
              sx={{
                display: {
                  sm: "flex-column",
                  md: "flex-row",
                },
                gap: 1,
              }}
            >
              <Input
                size="sm"
                placeholder="Insérer nom du Sujet"
                value={topic.titleText}
                onChange={(e) => {
                  // ... votre logique existante pour mettre à jour le titre
                }}
              />
            </FormControl>
          </Stack>

          {/** @DESCRIPTION_STACK */}
          <Stack spacing={2} sx={{ my: 1 }}>
            <Textarea
              size="sm"
              minRows={4}
              sx={{ mt: 1.5 }}
              placeholder="Entrer la description du sujet"
              value={topic.descriptionText}
              onChange={(e) => {
                // ... votre logique existante pour mettre à jour la description
              }}
            />
            <FormHelperText sx={{ mt: 0.75, fontSize: "xs" }}>
              {`${
                MAX_DESCRIPTION_LENGTH - topic.descriptionText.length
              } characters left`}
            </FormHelperText>
          </Stack>

          <Stack>
            <FormControl>
              <FormLabel>Catégories</FormLabel>
              <Select
                multiple
                value={topic.selectedCategories}
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
              >
                <Option value="WebDev">WebDev</Option>
                <Option value="Refactoring">Refactoring</Option>
                <Option value="Test Cases">Test Cases</Option>
                <Option value="Linux">Linux</Option>
              </Select>
            </FormControl>
          </Stack>

          <CardOverflow sx={{ borderTop: "1px solid", borderColor: "divider" }}>
            <CardActions sx={{ alignSelf: "flex-end", pt: 2 }}>
              <Button
                size="sm"
                variant="outlined"
                color="neutral"
                disabled={topics.length === 1 || topic.id < topics.length}
                onClick={() => handleDeleteTopic(topic.id)}
              >
                Supprimer Sujet
              </Button>
              <Button
                size="sm"
                variant="solid"
                onClick={handleAddTopic}
                disabled={topic.id < topics.length}
              >
                Ajouter Sujet
              </Button>
              {topic.id === topics.length && (
                <Button
                  size="sm"
                  variant="solid"
                  color="success"
                  onClick={submitSae}
                >
                  Créer SAE
                </Button>
              )}
            </CardActions>
          </CardOverflow>
        </Card>
      ))}
    </>
  );
}
