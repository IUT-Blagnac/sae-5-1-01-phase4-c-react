import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import SignInSide from "./pages/Login";
import Main from "./pages/Main";
import { CustomThemeProvider } from "./components/Header/ThemeContext";

function App() {
  return (
    <CustomThemeProvider>
      <Router>
        <Routes>
          <Route path="/" Component={SignInSide} />
          <Route path="/dashboard" Component={Main} />
        </Routes>
      </Router>
    </CustomThemeProvider>
  );
}

export default App;
