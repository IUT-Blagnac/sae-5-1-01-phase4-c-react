import { Box, Button, CssBaseline, Grid } from "@mui/material";
import thomasJpg from "../../../assets/img/thomas.webp";
import { URLs } from "../../../assets/enums/URLs.enum";

export default function EasterEgg() {
  const hasFoundEaster =
    localStorage.getItem("hasFoundEaster") === "SUPER_TESTA_ADMIN";

  if (!hasFoundEaster) window.location.href = URLs.BASE;

  return (
    <Grid container component="main" sx={{ height: "100vh" }}>
      <CssBaseline />
      <Box
        component="div"
        sx={{
          height: "100vh",
          width: "100%",
          backgroundImage: `url(${thomasJpg})`,
          backgroundRepeat: "no-repeat",
          backgroundSize: "cover",
          backgroundPosition: "center",
        }}
      >
        <Button
          onClick={() => {
            localStorage.removeItem("hasFoundEaster");
            window.location.href = URLs.BASE;
          }}
        >
          Revenir au login
        </Button>
      </Box>
    </Grid>
  );
}
