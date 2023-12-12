import { Box, CircularProgress, CssBaseline, CssVarsProvider } from "@mui/joy";
import BlankPage from "../pages/templates/BlankPage";

interface LoadingProps {
  showWholePage?: boolean;
}

export default function Loading({ showWholePage = true }: LoadingProps) {
  // Put the loading circle in the middle of the page
  return (
    <CssVarsProvider disableTransitionOnChange>
      <CssBaseline />
      <Box sx={{ display: "flex", minHeight: "100dvh" }}>
        <CircularProgress
          sx={{
            "--CircularProgress-size": "150px",
            "--CircularProgress-color": "var(--palette-primary-main)",
            position: "absolute",
            top: "50%",
            left: "50%",
            transform: "translate(-50%, -50%)",
          }}
        >
          Loading
        </CircularProgress>
      </Box>
    </CssVarsProvider>
  );
}
