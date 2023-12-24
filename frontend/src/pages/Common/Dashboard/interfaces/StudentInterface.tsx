import Typography from "@mui/joy/Typography";
import Table from "@mui/joy/Table";

// Icons import
import FolderRoundedIcon from "@mui/icons-material/FolderRounded";
import { Box } from "@mui/joy";
import { useEffect, useState } from "react";
import Sae from "../../../../models/Sae";
import Loading from "../../../../components/Loading";
import {
  convertSaeIntToStatutEnum,
  convertSaeStatutEnumToHText,
} from "../../../../utils/Utils";
import SaeServices from "../../../../middlewares/Services/Sae.Services";

function StudentInterface() {
  const [loading, setLoading] = useState(true);
  const [saes, setSaes] = useState<Sae[]>([]);

  useEffect(() => {
    const userId = localStorage.getItem("userid");
    const fetchData = async () => {
      setSaes(
        (await SaeServices.getAllSaeFromUserId(userId as string)) as Sae[]
      );
      setLoading(false);
    };
    fetchData();
  }, []);

  if (loading) return <Loading showWholePage={false} />;

  return (
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
          Mes SAEs
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
            </tr>
          </thead>
          <tbody>
            {saes.map((sae) => (
              <tr
                key={sae.id}
                onClick={() => {
                  window.location.href = `sae/${sae.id}/consult`;
                }}
                title="Cliquer pour consulter la SAE"
              >
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
                      // @ts-ignore
                      convertSaeIntToStatutEnum(sae?.state)
                    )}
                  </Typography>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      </Box>
    </Box>
  );
}

export default StudentInterface;
