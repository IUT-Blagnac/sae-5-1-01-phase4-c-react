import { useContext } from "react";

import { Button, AppBar, Toolbar, Typography, Switch } from "@mui/material";
import { ThemeContext } from "./ThemeContext";

import TeacherTab from "./Tab/TeacherTab";
import StudentTab from "./Tab/StudentTab";

interface HeaderProps {
  userStatus: string;
}

export default function Header(props: HeaderProps) {
  const { darkMode, toggleDarkMode } = useContext(ThemeContext);

  return (
    <AppBar
      position="static"
      color="default"
      elevation={0}
      sx={{ borderBottom: (theme) => `1px solid ${theme.palette.divider}` }}
    >
      <Toolbar sx={{ flexWrap: "wrap" }}>
        <Typography variant="h6" color="inherit" noWrap sx={{ flexGrow: 1 }}>
          CiSharp SAE
        </Typography>
        {props.userStatus === "admin" && <TeacherTab />}
        {props.userStatus === "student" && <StudentTab />}
        <Switch
          name="darkModeSwitch"
          color="primary"
          checked={darkMode}
          onChange={toggleDarkMode}
        />
        <Button href="#" variant="outlined" sx={{ my: 1, mx: 1.5 }}>
          Mon compte
        </Button>
      </Toolbar>
    </AppBar>
  );
}
