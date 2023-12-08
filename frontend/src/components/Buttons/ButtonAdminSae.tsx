import { Box, Button } from "@mui/joy";
import { SAEStatus } from "../../assets/enums/SAEStatus.enum";

const GenerateGroupsButton = (
  <Button
    variant="outlined"
    color="primary"
    size="sm"
    sx={{
      position: "absolute",
      top: 0,
      right: 0,
      margin: 2,
    }}
    title="Clôture les inscriptions et génère les groupes"
  >
    Générer les groupes
  </Button>
);

const GenerateWishesButton = (
  <Button
    variant="outlined"
    color="primary"
    size="sm"
    sx={{
      position: "absolute",
      top: 0,
      right: 0,
      margin: 2,
    }}
    title="Clôture les voeux et attribue les voeux"
  >
    Attribuer les voeux
  </Button>
);

const EndSaeButton = (
  <Button
    variant="outlined"
    color="primary"
    size="sm"
    sx={{
      margin: 2,
    }}
    title="Clôture la SAE"
  >
    Clôturer la SAE
  </Button>
);

const OpenForInternship = (
  <Button
    variant="outlined"
    color="primary"
    size="sm"
    sx={{
      margin: 2,
    }}
    title="Ouvre la SAE pour les alternants"
  >
    Ouvrir pour les alternants
  </Button>
);

interface ButtonAdminSaeProps {
  saeStatut: SAEStatus;
}

const ButtonAdminSae = ({ saeStatut }: ButtonAdminSaeProps): JSX.Element => {
  console.log(saeStatut);

  let buttons: JSX.Element;

  switch (saeStatut) {
    case SAEStatus.PENDING_USERS:
      buttons = GenerateGroupsButton;
      break;
    case SAEStatus.PENDING_WISHES:
      buttons = GenerateWishesButton;
      break;
    case SAEStatus.LAUNCHED:
      buttons = (
        <Box
          sx={{
            position: "absolute",
            top: 0,
            right: 0,
            margin: 2,
          }}
        >
          {EndSaeButton}
          {OpenForInternship}
        </Box>
      );
      break;
    case SAEStatus.LAUNCHED_OPEN_FOR_INTERNSHIP:
      buttons = EndSaeButton;
      break;
    default:
      buttons = <></>;
  }

  return buttons;
};

export default ButtonAdminSae;
