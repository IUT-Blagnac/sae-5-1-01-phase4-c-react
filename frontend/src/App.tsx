import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { URLs } from "./assets/enums/URLs.enum";

import SignInSide from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import CreateSAE from "./pages/Admin/CreateSAE";
import Support from "./pages/Support";
import ManageSAE from "./pages/Admin/ManageSAE";

function App() {
  return (
    <Router>
      <Routes>
        <Route path={URLs.BASE} Component={SignInSide} />
        <Route path={URLs.DASHBOARD} Component={Dashboard} />
        <Route path={URLs.CREATE_SAE} Component={CreateSAE} />
        <Route path={URLs.SUPPORT} Component={Support} />
        <Route path={URLs.SAE_MANAGE} Component={ManageSAE} />
      </Routes>
    </Router>
  );
}

export default App;
