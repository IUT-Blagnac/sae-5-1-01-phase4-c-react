import { Box, CircularProgress } from "@mui/joy";
import BlankPage from "../pages/templates/BlankPage";

export default function Loading() {
  return (
    <BlankPage pageTitle="Chargement ...">
      <Box>
        <Box
          sx={{
            display: "flex",
            alignItems: "center",
            py: 1,
            pl: { xs: 1, sm: 2 },
            pr: { xs: 1, sm: 1 },
          }}
        >
          <CircularProgress
            sx={{
              "--CircularProgress-size": "80px",
            }}
          >
            Load
          </CircularProgress>
        </Box>
      </Box>
    </BlankPage>
  );
}
