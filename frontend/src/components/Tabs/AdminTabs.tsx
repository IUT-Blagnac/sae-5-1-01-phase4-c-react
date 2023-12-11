import List from "@mui/joy/List";
import ListItem from "@mui/joy/ListItem";
import ListItemButton from "@mui/joy/ListItemButton";
import ListItemContent from "@mui/joy/ListItemContent";
import Typography from "@mui/joy/Typography";
import HomeRoundedIcon from "@mui/icons-material/HomeRounded";
import GroupRoundedIcon from "@mui/icons-material/GroupRounded";

import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import Toggler from "./TabToggler";
import { URLs } from "../../assets/enums/URLs.enum";
import { PlusOneRounded } from "@mui/icons-material";

export default function AdminTabs() {
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
        <ListItemButton
          selected={currentURL === URLs.DASHBOARD}
          onClick={() => {
            window.location.href = URLs.DASHBOARD;
          }}
        >
          <HomeRoundedIcon />
          <ListItemContent>
            <Typography level="title-sm">Home</Typography>
          </ListItemContent>
        </ListItemButton>
      </ListItem>

      <ListItem>
        {/** Href to sae */}
        <ListItemButton
          selected={currentURL === URLs.CREATE_SAE}
          onClick={() => {
            window.location.href = URLs.CREATE_SAE;
          }}
        >
          <PlusOneRounded />
          <ListItemContent>
            <Typography level="title-sm">Créer une SAE</Typography>
          </ListItemContent>
        </ListItemButton>
      </ListItem>

      <ListItem nested>
        <Toggler
          renderToggle={({ open, setOpen }) => (
            <ListItemButton
              onClick={() => setOpen(!open)}
              selected={currentURL.includes(URLs.STUDENTS) && !open}
            >
              <GroupRoundedIcon />
              <ListItemContent>
                <Typography level="title-sm">Étudiants</Typography>
              </ListItemContent>
              <KeyboardArrowDownIcon
                sx={{ transform: open ? "rotate(180deg)" : "none" }}
              />
            </ListItemButton>
          )}
        >
          <List sx={{ gap: 0.5 }}>
            <ListItem sx={{ mt: 0.5 }}>
              <ListItemButton
                role="menuitem"
                component="a"
                href={URLs.IMPORT_STUDENTS}
                selected={currentURL === URLs.IMPORT_STUDENTS}
              >
                Importer des utilisateurs
              </ListItemButton>
            </ListItem>
            <ListItem>
              <ListItemButton>Consulter un utilisateur</ListItemButton>
            </ListItem>
            <ListItem>
              <ListItemButton
                role="menuitem"
                component="a"
                href={URLs.CREATE_GROUP}
                selected={currentURL === URLs.CREATE_GROUP}
              >
                Créer un groupe
              </ListItemButton>
            </ListItem>
          </List>
        </Toggler>
      </ListItem>
    </List>
  );
}
