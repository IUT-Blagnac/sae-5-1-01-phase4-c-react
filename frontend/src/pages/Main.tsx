import Footer from "../components/Footer";
import Header from "../components/Header/Header";
import AuthChecker from "./AuthChecker";

export default function Main() {
  return (
    <AuthChecker>
      <Header userStatus={localStorage.getItem("statut") || ""} />
      <Footer />
    </AuthChecker>
  );
}
