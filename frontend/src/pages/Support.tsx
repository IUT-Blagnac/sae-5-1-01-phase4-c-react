import {
  Box,
  Button,
  Card,
  Checkbox,
  Chip,
  CssBaseline,
  CssVarsProvider,
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

import React from 'react';
import ReactDOM from 'react-dom';
import Slider from 'react-slick';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import { useState } from "react";
import Sidebar from "../components/SideBar";
import AuthChecker from "../middlewares/AuthChecker";
import Header from "../components/Header";
import Carrousel from "../components/Support/Carrousel";
import { Label } from "@mui/icons-material";

export default function Support() {
  // Paramètre du carrousel
  const settings = {
    infinite: true,
    slidesToShow: 3,
    slidesToScroll: 3,
    centerMode: true,
    centerPadding: '0',
  };

  // Pour afficher le formulaire
  const [afficherFormulaire, setAfficherFormulaire] = useState(false);

  const handleClick = () => {
    setAfficherFormulaire(true);
  };

  // Alerte quand le formulaire est soumis
  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    alert("Merci pour votre retour, nous vous répondrons prochainement par mail.");
    setAfficherFormulaire(false);
  };

  return (
    <AuthChecker>
      <CssVarsProvider disableTransitionOnChange>
        <CssBaseline />
        <Box sx={{ display: "flex", minHeight: "100dvh" }}>
          <Header />
          <Sidebar />
          <Box
            component="main"
            className="MainContent"
            sx={{
              pt: {
                xs: "calc(12px + var(--Header-height))",
                md: 3,
              },
              pb: {
                xs: 2,
                sm: 2,
                md: 3,
              },
              flex: 1,
              display: "flex",
              flexDirection: "column",
              minWidth: 0,
              height: "100dvh",
              gap: 1,
              overflow: "auto",
            }}
          >
            <Box
              sx={{
                flex: 1,
                width: "100%",
              }}
            >
              <Box
                sx={{
                  position: "sticky",
                  top: {
                    sm: -100,
                    md: -110,
                  },
                  bgcolor: "background.body",
                  zIndex: 9995,
                }}
              >
                <Box
                  sx={{
                    px: {
                      xs: 2,
                      md: 6,
                    },
                  }}
                >
                  <Typography
                    level="h2"
                    sx={{
                      mt: 1,
                      mb: 2,
                    }}
                  >
                    Support
                  </Typography>
                </Box>
              </Box>
              <Card
                color="primary"
                variant="soft"
                sx={{
                  margin: "0 1% 0 1%",
                }}
              >
                <Typography
                  level="h3"
                  sx={{
                    mt: 1,
                    mb: 2,
                    ml: 3,
                  }}
                >
                  FAQ
                </Typography>
                <Carrousel/>
              </Card>
            

              <Stack
                spacing={4}
                sx={{
                  display: "flex",
                  maxWidth: "800px",
                  mx: "auto",
                  px: {
                    xs: 2,
                    md: 6,
                  },
                  py: {
                    xs: 2,
                    md: 3,
                  },
                }}
              ></Stack>
                <Card
                  sx={{
                    margin: "0 1% 0 1%",
                  }}
                >
                  <Typography level="h3">
                    Besoin d'aide?
                  </Typography>
                  {!afficherFormulaire && (
                    <Button onClick={handleClick}>Contactez nous</Button>
                  )}
                  {afficherFormulaire && (
                    <Box sx={{ mb: 1 }}>
                      <form onSubmit={handleSubmit}>
                        <Stack spacing={1}>
                          Nom :
                          <Input placeholder="Nom" required />
                          Prénom :
                          <Input placeholder="Prénom" required />
                          Email :
                          <Input type="email" placeholder="Email" required />
                          <br/>
                          Sujet :
                          <Input placeholder="Sujet" required />
                          <br/>
                          Description :
                          <Textarea minRows={2} placeholder="Description..." required />
                          <br/>
                          <Checkbox label="En soumettant ce formulaire, j'accepte que les informations saisies soient exploitées dans le cadre de la demande de contact qui peut en découler." required />
                          <br/>
                          <Button type="submit">Envoyer</Button>
                        </Stack>
                      </form>
                    </Box>
                  )}
                </Card>
              
            </Box>
          </Box>
        </Box>
      </CssVarsProvider>
    </AuthChecker>
  );
}
