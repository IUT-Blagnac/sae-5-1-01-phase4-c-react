import Chip from "@mui/joy/Chip";
import List from "@mui/joy/List";
import ListItem from "@mui/joy/ListItem";
import ListItemButton from "@mui/joy/ListItemButton";
import ListItemContent from "@mui/joy/ListItemContent";
import Typography from "@mui/joy/Typography";
import HomeRoundedIcon from "@mui/icons-material/HomeRounded";
import DashboardRoundedIcon from "@mui/icons-material/DashboardRounded";
import AssignmentRoundedIcon from "@mui/icons-material/AssignmentRounded";
import { URLs } from "../../assets/enums/URLs.enum";

export default function StudentTabs() {
  const currentURL = window.location.pathname;

  return (
    <List
      size="sm"
      sx={{
        gap: 1,
        "--List-nestedInsetStart": "30px",
        "--ListItem-radius": (theme) => theme.vars.radius.sm,
      }}
    >
      <ListItem>
        <ListItemButton selected={currentURL === URLs.DASHBOARD}>
          <HomeRoundedIcon />
          <ListItemContent>
            <Typography level="title-sm">Home</Typography>
          </ListItemContent>
        </ListItemButton>
      </ListItem>

      <ListItem>
        <ListItemButton>
          <DashboardRoundedIcon />
          <ListItemContent>
            <Typography level="title-sm">Mes SAEs</Typography>
          </ListItemContent>
        </ListItemButton>
      </ListItem>

      <ListItem>
        <ListItemButton
          role="menuitem"
          component="a"
          selected={currentURL === URLs.SKILLS}
          href={URLs.SKILLS}
        >
          <AssignmentRoundedIcon />
          <ListItemContent>
            <Typography level="title-sm">Mes compétences</Typography>
          </ListItemContent>
          <Chip size="sm" color="primary" variant="solid">
            1
          </Chip>
        </ListItemButton>
      </ListItem>
    </List>
  );
}
