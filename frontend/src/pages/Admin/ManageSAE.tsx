import { Card } from "@mui/joy";
import { Status } from "../../assets/enums/Status.enum";
import BlankPage from "../templates/BlankPage";

export default function ManageSAE() {
  return (
    <BlankPage
      role={localStorage.getItem("statut") as Status}
      pageTitle="Consulter une SAE"
    >
      <Card></Card>
    </BlankPage>
  );
}
