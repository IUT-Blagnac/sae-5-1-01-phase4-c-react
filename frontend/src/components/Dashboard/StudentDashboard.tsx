import Typography from "@mui/joy/Typography";
import Table from "@mui/joy/Table";

// Icons import
import FolderRoundedIcon from "@mui/icons-material/FolderRounded";
import { Box } from "@mui/joy";

// custom

function StudentDashboard() {
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
                <Typography level="title-sm">Tâches à faire</Typography>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr
              onClick={() => {
                console.log("sae-koh-lanta");
              }}
            >
              <td>
                <Typography
                  level="title-sm"
                  startDecorator={<FolderRoundedIcon color="primary" />}
                  sx={{ alignItems: "flex-start" }}
                >
                  Koh-Lanta
                </Typography>
              </td>
              <td>
                <Typography level="body-sm">Useless</Typography>
              </td>
              <td>
                <Typography level="body-sm">Non</Typography>
              </td>
            </tr>
            <tr>
              <td>
                <Typography
                  level="title-sm"
                  startDecorator={<FolderRoundedIcon color="primary" />}
                  sx={{ alignItems: "flex-start" }}
                >
                  Slave-Narratives
                </Typography>
              </td>
              <td>
                <Typography level="body-sm">Say no to slavery</Typography>
              </td>
              <td>
                <Typography level="body-sm">Oui</Typography>
              </td>
            </tr>
          </tbody>
        </Table>
      </Box>
    </Box>
  );
}

export default StudentDashboard;
