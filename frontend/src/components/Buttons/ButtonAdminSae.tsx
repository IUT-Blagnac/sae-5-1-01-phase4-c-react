import { Button } from "@mui/joy";
import { SAEStatus } from "../../assets/enums/SAEStatus.enum";

interface ButtonAdminSaeProps {
  saeStatut: SAEStatus;
}
export default function ButtonAdminSae({ saeStatut }: ButtonAdminSaeProps) {
  let buttonText: string;
  let buttonHoverText: string;

  switch (saeStatut) {
    case SAEStatus.PENDING_USERS:
      buttonText = "Générer les groupes";
      buttonHoverText = "Clôture les inscriptions et génère les groupes";
      break;
    case SAEStatus.PENDING_WISHES:
      buttonText = "Attribuer les voeux";
      buttonHoverText = "Clôture les voeux et attribue les voeux";
      break;
    case SAEStatus.LAUNCHED:
      buttonText = "Clôturer la SAE";
      buttonHoverText = "Clôture la SAE";
      break;
    default:
      buttonText = "Erreur";
      buttonHoverText = "Erreur";
      break;
  }

  return (
    <Button
      color="primary"
      sx={{
        position: "absolute",
        top: 0,
        right: 0,
        margin: 2,
      }}
      title={buttonHoverText}
    >
      {buttonText}
    </Button>
  );
}
