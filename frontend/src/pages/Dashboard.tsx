import { Sheet } from "@mui/joy";

// Blank Page Template
import BlankPage from "./templates/BlankPage";

// Enums
import { Status } from "../assets/enums/Status.enum";

// Page Content
import StudentDashboard from "../components/Dashboard/StudentDashboard";
import AdminDashboard from "../components/Dashboard/AdminDashboard";

export default function Dashboard() {
  return (
    <BlankPage pageTitle={`Dashboard ${localStorage.getItem("statut")}`}>
      <Sheet
        variant="outlined"
        sx={{
          borderRadius: "sm",
          width: "100%", // La largeur du Sheet est de 100%
          maxWidth: "1200px", // Définissez une largeur maximale si nécessaire
        }}
      >
        {localStorage.getItem("statut") === Status.ADMIN && <AdminDashboard />}
        {localStorage.getItem("statut") === Status.STUDENT && (
          <StudentDashboard />
        )}
      </Sheet>
    </BlankPage>
  );
}
