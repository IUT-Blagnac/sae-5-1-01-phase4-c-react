import { CssVarsProvider } from "@mui/joy/styles";
import CssBaseline from "@mui/joy/CssBaseline";
import Box from "@mui/joy/Box";
import Typography from "@mui/joy/Typography";
import Sidebar from "../components/SideBar";
import Header from "../components/Header";
import AuthChecker from "../middlewares/AuthChecker";
import StudentDashboard from "../components/Dashboard/StudentDashboard";
import { Sheet } from "@mui/joy";
import { Status } from "../assets/enums/Status.enum";
import AdminDashboard from "../components/Dashboard/AdminDashboard";

export default function Dashboard() {
  return (
    <AuthChecker>
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
          >
            <Box
              sx={{
                display: "flex",
                my: 1,
                gap: 1,
                flexDirection: { xs: "column", sm: "row" },
                alignItems: { xs: "start", sm: "center" },
                flexWrap: "wrap",
                justifyContent: "space-between",
              }}
            >
              <Typography level="h2">
                Dashboard {" " + localStorage.getItem("statut")}
              </Typography>

              <Sheet
                variant="outlined"
                sx={{
                  borderRadius: "sm",
                  gridColumn: "1/-1",
                  display: { md: "flex" },
                }}
              >
                {localStorage.getItem("statut") === Status.ADMIN && (
                  <AdminDashboard />
                )}
                {localStorage.getItem("statut") === Status.STUDENT && (
                  <StudentDashboard />
                )}
              </Sheet>
            </Box>
          </Box>
        </Box>
      </CssVarsProvider>
    </AuthChecker>
  );
}
