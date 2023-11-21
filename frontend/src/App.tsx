// App.tsx
import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import SignInSide from "./pages/Login";
import Main from "./pages/Main";
import { ThemeProvider, createTheme } from "@mui/material/styles";
import { CustomThemeProvider } from "./components/Header/ThemeContext";

function App() {
  return (
    <CustomThemeProvider>
      <Router>
        <Routes>
          <Route path="/" Component={SignInSide} />
          <Route path="/main" Component={Main} />
        </Routes>
      </Router>
    </CustomThemeProvider>
  );
}

export default App;
