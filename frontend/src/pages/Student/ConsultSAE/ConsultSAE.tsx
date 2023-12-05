import { useEffect, useState } from "react";
import Sae from "../../../models/Sae";
import Topic from "../../../models/Topic";
import Loading from "../../../components/Loading";
import BlankPage from "../../templates/BlankPage";
import { Status } from "../../../assets/enums/Status.enum";
import AuthChecker from "../../../middlewares/AuthChecker";
import {
  Card,
  CardContent,
  CircularProgress,
  Divider,
  Typography,
} from "@mui/joy";
import API_URL from "../../../env";
import {
  convertSaeStatutEnumToHText,
  convertSaeIntToStatutEnum,
  getFetchHeaders,
} from "../../../utils/Utils";
import { DocumentScannerOutlined } from "@mui/icons-material";

export default function ConsultSAE() {
  const [loading, setLoading] = useState(true);
  const [sae, setSae] = useState<Sae>() as [
    Sae,
    React.Dispatch<React.SetStateAction<Sae>>
  ];
  const [topics, setTopics] = useState<Topic[]>([]);

  useEffect(() => {
    let saeId = window.location.href.split("/")[4];
    fetch(API_URL + "/api/Subject/sae/" + saeId, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    }).then((res) => {
      if (res.status === 200) {
        res.json().then((data) => {
          setTopics(data);
          fetch(API_URL + "/api/Sae/user/" + localStorage.getItem("userid"), {
            method: "GET",
            headers: getFetchHeaders(),
          }).then((res) => {
            if (res.status === 200) {
              res.json().then((data) => {
                let saes = data as Sae[];
                setSae(saes.find((sae) => sae.id === saeId) as Sae);
                setLoading(false);
              });
            }
          });
        });
      }
    });
  }, []);

  if (loading) return <Loading />;

  return (
    <AuthChecker>
      <BlankPage role={Status.STUDENT} pageTitle="Consulter une SAE">
        <Card variant="solid" color="primary" invertedColors>
          <CardContent orientation="horizontal">
            <CircularProgress size="lg" determinate value={33}>
              <DocumentScannerOutlined />
            </CircularProgress>
            <CardContent>
              <Typography level="body-md">IUT Blagnac</Typography>
              <Typography level="h2">{sae?.name}</Typography>
              <Typography level="body-md">
                {convertSaeStatutEnumToHText(
                  // @ts-ignore
                  convertSaeIntToStatutEnum(sae?.statut)
                )}
              </Typography>
              <Typography level="body-sm">{sae?.description}</Typography>
            </CardContent>
          </CardContent>
        </Card>
        <Divider sx={{ mt: 2, mb: 2 }} />
        {topics.map((topic) => (
          <Card
            key={topic.id}
            sx={{
              mt: 2,
              ":hover": {
                cursor: "pointer",
                border: "1px solid",
              },
            }}
          >
            <Typography level="title-sm" sx={{ mb: 2 }}>
              {topic.name}
            </Typography>
            <Typography level="body-sm" sx={{ mb: 2 }}>
              {topic.description}
            </Typography>
          </Card>
        ))}
      </BlankPage>
    </AuthChecker>
  );
}
