// App.tsx
import React from "react";
import SignInSide from "./pages/Login";
import { ThemeProvider, createTheme } from "@mui/material/styles";

function App() {
  const defaultTheme = createTheme();

  return (
    <ThemeProvider theme={defaultTheme}>
      <SignInSide />
    </ThemeProvider>
  );
}

export default App;
