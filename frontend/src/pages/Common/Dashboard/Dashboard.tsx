import { Sheet } from "@mui/joy";

// Blank Page Template
import BlankPage from "../../templates/BlankPage";

// Enums
import { Status } from "../../../assets/enums/Status.enum";

// Page Content
import StudentInterface from "./interfaces/StudentInterface";
import AdminInterface from "./interfaces/AdminInterface";

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
        {localStorage.getItem("statut") === Status.ADMIN && <AdminInterface />}
        {localStorage.getItem("statut") === Status.STUDENT && (
          <StudentInterface />
        )}
      </Sheet>
    </BlankPage>
  );
}
