import { Card, Divider, Typography } from "@mui/joy";
import { Status } from "../../../assets/enums/Status.enum";
import BlankPage from "../../templates/BlankPage";
import { useEffect, useState } from "react";
import Sae from "../../../models/Sae";
import Topic from "../../../models/Topic";
import Loading from "../../../components/Loading";
import {
  convertSaeIntToStatutEnum,
  convertSaeStatutEnumToHText,
  getFetchHeaders,
} from "../../../utils/Utils";
import ButtonAdminSae from "../../../components/Buttons/ButtonAdminSae";
import { SAEStatus } from "../../../assets/enums/SAEStatus.enum";
import PendingUsers from "./interfaces/PendingUsers";
import PendingWishes from "./interfaces/PendingWishes";
import AuthChecker from "../../../middlewares/AuthChecker";
import API_URL from "../../../env";

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
    fetch(API_URL + "/api/Sae/" + saeId, {
      method: "GET",
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    })
      .then((response) => {
        if (response.status === 200) {
          response.json().then((data) => {
            console.log(data);

            setSae(data);
            fetch(API_URL + "/api/Subject/sae/" + saeId, {
              method: "GET",
              headers: getFetchHeaders(),
            }).then((res) => {
              res.json().then((data) => {
                setTopics(data);
                console.log(sae);
                setLoading(false);
              });
            });
          });
        }
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  if (loading) return <Loading />;

  return (
    <AuthChecker>
      <BlankPage role={Status.ADMIN} pageTitle="Manager une SAE">
        <Card>
          <ButtonAdminSae saeStatut={convertSaeIntToStatutEnum(sae?.state)} />
          <Typography level="h3">
            {sae?.name} :{" "}
            {convertSaeStatutEnumToHText(convertSaeIntToStatutEnum(sae?.state))}
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

          {convertSaeIntToStatutEnum(sae?.state) ===
            SAEStatus.PENDING_USERS && <PendingUsers topics={topics} />}
          {convertSaeIntToStatutEnum(sae?.state) ===
            SAEStatus.PENDING_WISHES && <PendingWishes topics={topics} />}
        </Card>
      </BlankPage>
    </AuthChecker>
  );
}
