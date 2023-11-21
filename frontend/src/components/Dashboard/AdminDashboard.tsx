import Typography from "@mui/joy/Typography";
import Table from "@mui/joy/Table";

// Icons import
import FolderRoundedIcon from "@mui/icons-material/FolderRounded";
import { Box, Button } from "@mui/joy";

// custom

function AdminDashboard() {
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
                <Typography level="title-sm">Total Groupes</Typography>
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
                <Typography level="body-sm">5</Typography>
              </td>
              <td>
                <Typography level="body-sm">36</Typography>
              </td>
              <td>
                <Button variant="outlined" color="primary" size="sm">
                  Évoluer
                </Button>
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
                <Typography level="body-sm">7</Typography>
              </td>
              <td>
                <Typography level="body-sm">21</Typography>
              </td>
              <td>
                <Button variant="outlined" color="primary" size="sm">
                  Évoluer
                </Button>
              </td>
            </tr>
          </tbody>
        </Table>
      </Box>
    </Box>
  );
}

export default AdminDashboard;
