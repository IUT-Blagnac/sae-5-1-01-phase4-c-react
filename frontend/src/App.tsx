import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import SignInSide from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import { URLs } from "./assets/enums/URLs.enum";
import CreateSAE from "./pages/Admin/CreateSAE";
import Support from "./pages/Support";

function App() {
  return (
    <Router>
      <Routes>
        <Route path={URLs.BASE} Component={SignInSide} />
        <Route path={URLs.DASHBOARD} Component={Dashboard} />
        <Route path={URLs.CREATE_SAE} Component={CreateSAE} />
        <Route path={URLs.SUPPORT} Component={Support} />
      </Routes>
    </Router>
  );
}

export default App;
