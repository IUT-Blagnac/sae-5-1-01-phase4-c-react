import * as React from "react";
import {
  Button,
  Avatar,
  CssBaseline,
  TextField,
  Checkbox,
  Link,
  Paper,
  Box,
  Grid,
  FormControlLabel,
  Typography,
} from "@mui/material";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import loginImage from "../assets/img/login.jpg";
import Copyright from "../components/Copyright";
import { URLs } from "../assets/enums/URLs.enum";
import API_URL from "../env";

export default function SignInSide() {
  localStorage.removeItem("hasFoundEaster");
  const [error, setError] = React.useState<string | null>(null);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const data = new FormData(event.currentTarget);
    const body = {
      email: data.get("email"),
      password: data.get("password"),
    };

    if (data.get("email") === "Thomas" && data.get("password") === "Testa") {
      localStorage.setItem("hasFoundEaster", "SUPER_TESTA_ADMIN");
      window.location.href = URLs.EASTER;
      return;
    }

    try {
      const resLogin = await fetch(API_URL + "/api/Auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(body),
      });

      if (resLogin.status === 200) {
        const resultLogin = await resLogin.json();
        localStorage.setItem("token", resultLogin.token);

        const resUser = await fetch(API_URL + "/api/User/currentUser", {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        });

        setError("");
        const resultUser = await resUser.json();

        localStorage.setItem("userid", resultUser.id);
        localStorage.setItem("email", resultUser.email);
        localStorage.setItem("firstname", resultUser.firstname);
        localStorage.setItem("lastname", resultUser.lastname);
        localStorage.setItem("statut", resultUser.role);

        fetch(API_URL + `/api/Character/user/${resultUser.id}`, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        }).then(async (res) => {
          if (res.status === 200) {
            const data = await res.json();
            if (data.length === 0)
              localStorage.setItem(
                "hasDoneSkills",
                data.length === 0 ? "false" : "true"
              );
          } else {
            throw new Error("Erreur lors de la récupération des compétences");
          }
        });

        window.location.href = "/dashboard";
      } else {
        // Afficher un message d'erreur en cas d'échec de connexion
        setError("La connexion a échoué. Vérifiez vos identifiants.");
      }
    } catch (e) {
      setError("Une erreur inattendue s'est produite. Veuillez réessayer.");
    }
  };

  return (
    <Grid container component="main" sx={{ height: "100vh" }}>
      <CssBaseline />
      <Grid
        item
        xs={false}
        sm={4}
        md={7}
        sx={{
          backgroundImage: `url(${loginImage})`,
          backgroundRepeat: "no-repeat",
          backgroundColor: (t) =>
            t.palette.mode === "light"
              ? t.palette.grey[50]
              : t.palette.grey[900],
          backgroundSize: "cover",
          backgroundPosition: "center",
        }}
      />
      <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
        <Box
          sx={{
            my: 8,
            mx: 4,
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h2" variant="h4">
            CiReact SAE
          </Typography>
          <Typography component="h2" variant="h5">
            Se connecter
          </Typography>
          {error && (
            <Typography color="error" sx={{ mt: 1 }}>
              {error}
            </Typography>
          )}
          <Box
            component="form"
            noValidate
            sx={{ mt: 1 }}
            onSubmit={handleSubmit}
          >
            <TextField
              margin="normal"
              required
              fullWidth
              id="email"
              label="Adresse Email"
              name="email"
              autoComplete="email"
              autoFocus
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="password"
              label="Mot de passe"
              type="password"
              id="password"
              autoComplete="current-password"
            />
            <FormControlLabel
              control={<Checkbox value="remember" color="primary" />}
              label="Se souvenir de moi"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Se connecter
            </Button>
            <Grid container>
              <Grid item xs>
                <Link href="#" variant="body2">
                  Mot de passe oublié ?
                </Link>
              </Grid>
            </Grid>
            <Copyright sx={{ mt: 5 }} align="center" />
          </Box>
        </Box>
      </Grid>
    </Grid>
  );
}
