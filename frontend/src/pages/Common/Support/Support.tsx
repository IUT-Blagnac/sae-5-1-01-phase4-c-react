import {
  Box,
  Button,
  Card,
  CardContent,
  Divider,
  FormControl,
  Input,
  Table,
  Textarea,
  Typography,
} from "@mui/joy";

import BlankPage from "../../templates/BlankPage";
import { ArrowForward } from "@mui/icons-material";
import { Status } from "../../../assets/enums/Status.enum";

export default function Support() {
  return (
    <BlankPage pageTitle="Support">
      <Card>
        <Typography level="h3" sx={{ mb: 2 }}>
          États des SAEs
        </Typography>
        <CardContent>
          <Table
            hoverRow
            size="sm"
            borderAxis="none"
            variant="soft"
            sx={{
              "--TableCell-paddingX": "1rem",
              "--TableCell-paddingY": "1rem",
            }}
          >
            <thead>
              <tr>
                <th>
                  <Typography level="title-sm">Nom</Typography>
                </th>
                <th>
                  <Typography level="title-sm">Description</Typography>
                </th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>
                  <Typography
                    level="title-sm"
                    startDecorator={<ArrowForward color="primary" />}
                    sx={{ alignItems: "flex-start" }}
                  >
                    En attente du remplissage des fiches étudiantes
                  </Typography>
                </td>
                <td>
                  <Typography level="body-sm">
                    Cela signifie que l'enseignant attend que les étudiants
                    remplissent leurs fiches de compétences avant de générer les
                    groupes automatiquement.
                  </Typography>
                </td>
              </tr>
              <tr>
                <td>
                  <Typography
                    level="title-sm"
                    startDecorator={<ArrowForward color="primary" />}
                    sx={{ alignItems: "flex-start" }}
                  >
                    En attente du remplissage des voeux
                  </Typography>
                </td>
                <td>
                  <Typography level="body-sm">
                    Cela signifie que l'enseignant attend qu'un étudiant par
                    groupe choisisse ses voeux parmi les sujets proposés.
                  </Typography>
                </td>
              </tr>
              <tr>
                <td>
                  <Typography
                    level="title-sm"
                    startDecorator={<ArrowForward color="primary" />}
                    sx={{ alignItems: "flex-start" }}
                  >
                    Lancée
                  </Typography>
                </td>
                <td>
                  <Typography level="body-sm">
                    Cela signifie que l'enseignant a lancé l'association des
                    sujets, les étudiants peuvent alors consulter leurs groupes
                    et leurs sujets.
                  </Typography>
                </td>
              </tr>
              <tr>
                <td>
                  <Typography
                    level="title-sm"
                    startDecorator={<ArrowForward color="primary" />}
                    sx={{ alignItems: "flex-start" }}
                  >
                    Lancée et ouverte aux alternants
                  </Typography>
                </td>
                <td>
                  <Typography level="body-sm">
                    Cela signifie que l'enseignant a rouvert l'ajout des
                    alternants aux groupes.
                  </Typography>
                </td>
              </tr>
              <tr>
                <td>
                  <Typography
                    level="title-sm"
                    startDecorator={<ArrowForward color="primary" />}
                    sx={{ alignItems: "flex-start" }}
                  >
                    Clôturée
                  </Typography>
                </td>
                <td>
                  <Typography level="body-sm">
                    Cela signifie que la SAE est terminée, les étudiants ne
                    peuvent plus rien faire.
                  </Typography>
                </td>
              </tr>
            </tbody>
          </Table>
        </CardContent>
      </Card>
      {localStorage.getItem("statut") === Status.STUDENT && (
        <>
          <Divider sx={{ my: 4 }} />
          <Card>
            <Typography level="h3" sx={{ mb: 2 }}>
              Formulaire de contact
            </Typography>
            <CardContent>
              <FormControl>
                <Box display="flex" flexDirection="row">
                  <Input
                    placeholder="Prénom"
                    name="firstname"
                    sx={{ mb: 2, mr: 2, flex: 1 }}
                  />
                  <Input
                    placeholder="Nom"
                    name="lastname"
                    sx={{ mb: 2, flex: 1 }}
                  />
                </Box>
                <Input
                  placeholder="Email"
                  name="email"
                  sx={{ mb: 2, width: "100%" }}
                />
                <Textarea
                  placeholder="Votre message"
                  minRows={3}
                  required
                  sx={{ mb: 3, width: "100%" }}
                />
              </FormControl>
            </CardContent>
            <Button color="success" sx={{ ml: 2, mr: "auto", mb: 2 }}>
              Envoyer demande de support
            </Button>
          </Card>
        </>
      )}
    </BlankPage>
  );
}
