import Typography from "@mui/joy/Typography";
import Table from "@mui/joy/Table";

// Icons import
import FolderRoundedIcon from "@mui/icons-material/FolderRounded";
import { Box, Button, Sheet } from "@mui/joy";
import { useEffect, useState } from "react";
import Sae from "../../../../models/Sae";
import Loading from "../../../../components/Loading";
import {
  convertSaeIntToStatutEnum,
  convertSaeStatutEnumToHText,
} from "../../../../utils/Utils";
import SaeServices from "../../../../middlewares/Services/Sae.Services";
import BlankPage from "../../../templates/BlankPage";

function AdminInterface() {
  const [loading, setLoading] = useState(true);
  const [saes, setSaes] = useState<Sae[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      setSaes((await SaeServices.getSaeInfoFromAdminUserId()) as Sae[]);
      setTimeout(() => {
        setLoading(false);
      }, 4000);
    };
    fetchData();
  }, []);

  if (loading) return <Loading showWholePage={false} />;

  return (
    <BlankPage pageTitle="Dashboard">
      <Sheet
        variant="outlined"
        sx={{
          borderRadius: "sm",
          width: "100%", // La largeur du Sheet est de 100%
          maxWidth: "1200px", // Définissez une largeur maximale si nécessaire
        }}
      >
        <Box>
          <Box
            sx={{
              display: "flex",
              alignItems: "center",
              py: 1,
              pl: { xs: 1, sm: 2 },
              pr: { xs: 1, sm: 1 },
              borderTopLeftRadius: "var(--unstable_actionRadius)",
              borderTopRightRadius: "var(--unstable_actionRadius)",
            }}
          >
            <Typography
              level="body-lg"
              sx={{ flex: "1 1 100%" }}
              id="tableTitle"
              component="div"
            >
              SAEs
            </Typography>
          </Box>
          <Box>
            <Table
              hoverRow
              size="sm"
              borderAxis="none"
              variant="soft"
              sx={{
                "--TableCell-paddingX": "1rem",
                "--TableCell-paddingY": "1rem",
              }}
            >
              <thead>
                <tr>
                  <th>
                    <Typography level="title-sm">Nom</Typography>
                  </th>
                  <th>
                    <Typography level="title-sm">Description</Typography>
                  </th>
                  <th>
                    <Typography level="title-sm">État de la SAE</Typography>
                  </th>
                  <th>
                    <Typography level="title-sm">Total Étudiants</Typography>
                  </th>
                  <th>
                    <Typography level="title-sm">Faire évoluer</Typography>
                  </th>
                </tr>
              </thead>
              <tbody>
                {saes.map((sae) => (
                  <tr key={sae.id}>
                    <td>
                      <Typography
                        level="title-sm"
                        startDecorator={<FolderRoundedIcon color="primary" />}
                        sx={{ alignItems: "flex-start" }}
                      >
                        {sae.name}
                      </Typography>
                    </td>
                    <td>
                      <Typography level="body-sm">{sae.description}</Typography>
                    </td>
                    <td>
                      <Typography level="body-sm">
                        {convertSaeStatutEnumToHText(
                          convertSaeIntToStatutEnum(sae.state)
                        )}
                      </Typography>
                    </td>
                    <td>
                      <Typography level="body-sm">{2}</Typography>
                    </td>
                    <td>
                      <Button
                        variant="outlined"
                        color="primary"
                        size="sm"
                        onClick={() => {
                          window.location.href = `sae/${sae.id}/manage`;
                        }}
                      >
                        Évoluer
                      </Button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </Box>
        </Box>
      </Sheet>
    </BlankPage>
  );
}

export default AdminInterface;
