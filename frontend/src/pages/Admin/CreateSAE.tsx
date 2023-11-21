import { Box, CssBaseline, CssVarsProvider } from "@mui/joy";
import AuthChecker from "../../middlewares/AuthChecker";
import AdminOnly from "../../middlewares/AdminOnly";
import Header from "../../components/Header";
import Sidebar from "../../components/SideBar";

export default function CreateSAE() {
  return (
    <AuthChecker>
      <AdminOnly>
        <CssVarsProvider disableTransitionOnChange>
          <CssBaseline />
          <Header />
          <Box sx={{ display: "flex", minHeight: "100dvh" }}>
            <Sidebar />
            <Box
              component="main"
              className="MainContent"
              sx={{
                px: {
                  xs: 2,
                  md: 6,
                },
                pt: {
                  xs: "calc(12px + var(--Header-height))",
                  sm: "calc(12px + var(--Header-height))",
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
              }}
            ></Box>
          </Box>
        </CssVarsProvider>
      </AdminOnly>
    </AuthChecker>
  );
}
