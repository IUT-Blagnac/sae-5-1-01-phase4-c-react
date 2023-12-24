import React from "react";
import { Link } from "react-router-dom";
import { Button, Typography, Container } from "@mui/material";
import { URLs } from "../assets/enums/URLs.enum";

const NotFoundPage: React.FC = () => {
  return (
    <Container
      maxWidth="md"
      style={{ textAlign: "center", marginTop: "100px" }}
    >
      <Typography variant="h1" color="primary" gutterBottom>
        404
      </Typography>
      <Typography variant="h4" color="textSecondary" paragraph>
        Oops! La page que vous recherchez semble introuvable.
      </Typography>
      <Typography variant="body1" color="textSecondary" paragraph>
        Il semble que vous ayez suivi un lien incorrect. Revenez à la{" "}
        <Link to={URLs.DASHBOARD} style={{ textDecoration: "none" }}>
          page d'accueil
        </Link>{" "}
        pour trouver ce que vous cherchez.
      </Typography>
      <Button
        variant="contained"
        color="primary"
        component={Link}
        to={URLs.DASHBOARD}
      >
        Retour à la page d'accueil
      </Button>
    </Container>
  );
};

export default NotFoundPage;
