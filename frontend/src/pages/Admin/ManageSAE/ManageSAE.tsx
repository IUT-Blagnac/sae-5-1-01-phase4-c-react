import { Card, Divider, Typography } from "@mui/joy";
import { Status } from "../../../assets/enums/Status.enum";
import BlankPage from "../../templates/BlankPage";
import { useEffect, useState } from "react";
import Sae from "../../../models/Sae";
import Topic from "../../../models/Topic";
import FetchData from "../../../assets/temp/FetchData";
import Loading from "../../../components/Loading";
import { convertSaeStatutEnumToHText } from "../../../utils/Utils";
import ButtonAdminSae from "../../../components/Buttons/ButtonAdminSae";
import { SAEStatus } from "../../../assets/enums/SAEStatus.enum";
import PendingUsers from "./interfaces/PendingUsers";
import PendingWishes from "./interfaces/PendingWishes";
import AuthChecker from "../../../middlewares/AuthChecker";

export default function ConsultSAE() {
  const saeId = window.location.href.split("/")[4];
  const [loading, setLoading] = useState(true);
  const [sae, setSae] = useState<Sae>() as [
    Sae,
    React.Dispatch<React.SetStateAction<Sae>>
  ];
  const [topics, setTopics] = useState<Topic[]>([]);
  const [signedStudent, setSignedStudent] = useState<any[]>([]);

  useEffect(() => {
    FetchData.fetchSae(saeId).then((data) => {
      setSae(data);

      FetchData.fetchTopics(saeId).then((data) => {
        setTopics(data);

        setSignedStudent([]);
        setLoading(false);
      });
    });
  });

  if (loading) return <Loading />;

  return (
    <AuthChecker>
      <BlankPage role={Status.ADMIN} pageTitle="Manager une SAE">
        <Card>
          <ButtonAdminSae saeStatut={sae?.statut} />
          <Typography level="h3">
            {sae?.name} : {convertSaeStatutEnumToHText(sae?.statut)}
          </Typography>
          <Typography level="h4">
            {signedStudent.length} étudiants inscrits
          </Typography>
          <Typography
            sx={{
              whiteSpace: "pre-wrap",
            }}
          >
            {sae?.description}
          </Typography>

          <Divider sx={{ my: 2 }} />

          {sae?.statut === SAEStatus.PENDING_USERS && (
            <PendingUsers topics={topics} />
          )}
          {sae?.statut === SAEStatus.PENDING_WISHES && (
            <PendingWishes topics={topics} />
          )}
        </Card>
      </BlankPage>
    </AuthChecker>
  );
}
