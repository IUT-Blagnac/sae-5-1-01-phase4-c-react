import { useEffect, useState } from "react";
import Sae from "../../../models/Sae";
import Topic from "../../../models/Topic";
import FetchData from "../../../assets/temp/FetchData";
import Loading from "../../../components/Loading";
import BlankPage from "../../templates/BlankPage";
import { Status } from "../../../assets/enums/Status.enum";
import AuthChecker from "../../../middlewares/AuthChecker";
import { Card } from "@mui/joy";
import API_URL from "../../../env";

export default function ConsultSAE() {
  const saeId = window.location.href.split("/")[4];
  const [loading, setLoading] = useState(true);
  const [sae, setSae] = useState<Sae>() as [
    Sae,
    React.Dispatch<React.SetStateAction<Sae>>
  ];
  const [topics, setTopics] = useState<Topic[]>([]);

  useEffect(() => {
    let saeId = window.location.href.split("/")[4];
    fetch(API_URL + "/api/Sae/user/" + localStorage.getItem("userid"), {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    }).then((res) => {
      console.log(res);
    });
  });

  if (loading) return <Loading />;

  return (
    <AuthChecker>
      <BlankPage role={Status.STUDENT} pageTitle="Consulter une SAE">
        <Card></Card>
      </BlankPage>
    </AuthChecker>
  );
}
