import { Card } from "@mui/joy";
import { Status } from "../../assets/enums/Status.enum";
import BlankPage from "../templates/BlankPage";

export default function ConsultSAE() {
  return (
    <BlankPage
      role={localStorage.getItem("statut") as Status}
      pageTitle="Consulter une SAE"
    >
      <Card></Card>
    </BlankPage>
  );
}
