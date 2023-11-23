import GlobalStyles from "@mui/joy/GlobalStyles";
import Box from "@mui/joy/Box";
import Divider from "@mui/joy/Divider";
import IconButton from "@mui/joy/IconButton";
import List from "@mui/joy/List";
import ListItem from "@mui/joy/ListItem";
import ListItemButton, { listItemButtonClasses } from "@mui/joy/ListItemButton";
import Typography from "@mui/joy/Typography";
import Sheet from "@mui/joy/Sheet";
import SupportRoundedIcon from "@mui/icons-material/SupportRounded";
import SettingsRoundedIcon from "@mui/icons-material/SettingsRounded";

import ColorSchemeToggle from "./ColorSchemeToggle";
import { closeSidebar } from "../utils";
import { Folder, Man2Rounded } from "@mui/icons-material";
import AdminTabs from "./Tabs/AdminTabs";
import { Status } from "../assets/enums/Status.enum";
import StudentTabs from "./Tabs/StudentTabs";
import { Button } from "@mui/joy";
import { URLs } from "../assets/enums/URLs.enum";

export default function Sidebar() {
  const handleSignOut = () => {
    localStorage.clear();
    window.location.href = "/";
  };

  const currentURL = window.location.pathname;

  return (
    <Sheet
      className="Sidebar"
      sx={{
        position: {
          xs: "fixed",
          md: "sticky",
        },
        transform: {
          xs: "translateX(calc(100% * (var(--SideNavigation-slideIn, 0) - 1)))",
          md: "none",
        },
        transition: "transform 0.4s, width 0.4s",
        zIndex: 10000,
        height: "100dvh",
        width: "var(--Sidebar-width)",
        top: 0,
        p: 2,
        flexShrink: 0,
        display: "flex",
        flexDirection: "column",
        gap: 2,
        borderRight: "1px solid",
        borderColor: "divider",
      }}
    >
      <GlobalStyles
        styles={(theme) => ({
          ":root": {
            "--Sidebar-width": "220px",
            [theme.breakpoints.up("lg")]: {
              "--Sidebar-width": "240px",
            },
          },
        })}
      />
      <Box
        className="Sidebar-overlay"
        sx={{
          position: "fixed",
          zIndex: 9998,
          top: 0,
          left: 0,
          width: "100vw",
          height: "100vh",
          opacity: "var(--SideNavigation-slideIn)",
          backgroundColor: "var(--joy-palette-background-backdrop)",
          transition: "opacity 0.4s",
          transform: {
            xs: "translateX(calc(100% * (var(--SideNavigation-slideIn, 0) - 1) + var(--SideNavigation-slideIn, 0) * var(--Sidebar-width, 0px)))",
            lg: "translateX(-100%)",
          },
        }}
        onClick={() => closeSidebar()}
      />
      <Box sx={{ display: "flex", gap: 1, alignItems: "center" }}>
        <IconButton variant="soft" color="primary" size="sm">
          <Folder />
        </IconButton>
        <Typography level="title-lg">CiReact SAE</Typography>
        <ColorSchemeToggle sx={{ ml: "auto" }} />
      </Box>

      <Box
        sx={{
          minHeight: 0,
          overflow: "hidden auto",
          flexGrow: 1,
          display: "flex",
          flexDirection: "column",
          [`& .${listItemButtonClasses.root}`]: {
            gap: 1.5,
          },
        }}
      >
        {localStorage.getItem("statut") === Status.ADMIN && <AdminTabs />}
        {localStorage.getItem("statut") === Status.STUDENT && <StudentTabs />}
        <List
          size="sm"
          sx={{
            mt: "auto",
            flexGrow: 0,
            "--ListItem-radius": (theme) => theme.vars.radius.sm,
            "--List-gap": "8px",
          }}
        >
          <ListItem>
            <ListItemButton
              onClick={() => {
                window.location.href = URLs.SUPPORT;
              }}
              selected={currentURL === URLs.SUPPORT}
            >
              <SupportRoundedIcon />
              Support
            </ListItemButton>
          </ListItem>
          <ListItem>
            <ListItemButton>
              <SettingsRoundedIcon />
              Settings
            </ListItemButton>
          </ListItem>
          <ListItem>
            <Button
              size="md"
              variant="outlined"
              color="danger"
              onClick={handleSignOut}
            >
              Déconnexion
            </Button>
          </ListItem>
        </List>
      </Box>
      <Divider />
      <Box sx={{ display: "flex", gap: 1, alignItems: "center" }}>
        <Man2Rounded />
        <Box sx={{ minWidth: 0, flex: 1 }}>
          <Typography level="title-sm">
            {`${localStorage.getItem("firstname")} ${
              localStorage.getItem("lastname")?.[0]
            }.`}
          </Typography>
          <Typography level="body-xs">
            {localStorage.getItem("email")}
          </Typography>
        </Box>
        {/* <IconButton size="sm" variant="plain" color="neutral">
          <LogoutRoundedIcon />
        </IconButton> */}
      </Box>
    </Sheet>
  );
}
