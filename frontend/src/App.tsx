import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { URLs } from "./assets/enums/URLs.enum";

import SignInSide from "./pages/Login";
import Dashboard from "./pages/Common/Dashboard/Dashboard";
import CreateSAE from "./pages/Admin/CreateSAE/CreateSAE";
import Support from "./pages/Common/Support/Support";
import ConsultSAE from "./pages/Admin/ConsultSAE/ConsultSAE";
import Skill from "./pages/Student/Skill";
import NotFoundPage from "./pages/404";
import ImportUser from "./pages/Admin/ImportUser/ImportUser";

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
