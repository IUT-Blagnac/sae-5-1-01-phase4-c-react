import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { URLs } from "./assets/enums/URLs.enum";

import SignInSide from "./pages/Login";
import Main from "./pages/Main";
import { CustomThemeProvider } from "./components/Header/ThemeContext";

function App() {
  return (
    <Router>
      <Routes>
        {/** Common routes */}
        <Route path={URLs.BASE} Component={SignInSide} />
        <Route path={URLs.SUPPORT} Component={Support} />
        <Route path={URLs.DASHBOARD} Component={Dashboard} />
        <Route path="*" Component={NotFoundPage} />

        {/** Admin Only routes */}
        <Route path={URLs.CREATE_SAE} Component={CreateSAE} />
        <Route path={URLs.SAE_MANAGE} Component={ConsultSAE} />
        <Route path={URLs.IMPORT_STUDENTS} Component={ImportUser} />

        {/** Student Only routes */}
        <Route path={URLs.SKILLS} Component={Skill} />

        {/** Consultant Only routes */}
      </Routes>
    </Router>
  );
}

export default App;
