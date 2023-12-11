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
import TopicServices from "../../../middlewares/Services/Topic.Services";
import SaeServices from "../../../middlewares/Services/Sae.Services";
import TeamServices, {
  User,
} from "../../../middlewares/Services/Team.Services";

export default function ConsultSAE() {
  const [loading, setLoading] = useState(true);
  const [sae, setSae] = useState<Sae>() as [
    Sae,
    React.Dispatch<React.SetStateAction<Sae>>
  ];
  const [topics, setTopics] = useState<Topic[]>([]);
  const [team, setTeam] = useState<{
    idTeam: string;
    nameTeam: string;
    colorTeam: string;
    users: User[];
  }>();

  useEffect(() => {
    const saeId = window.location.href.split("/")[4];
    const userId = localStorage.getItem("userid") as string;

    const fetchData = async () => {
      const sae = await SaeServices.getSaeInfoFromUserId(userId, saeId);
      setSae(sae);
      setTopics(await TopicServices.getTopicsFromSae(saeId));

      if (sae?.state >= 1) {
        setTeam(await TeamServices.getTeamFromUserSae(userId, saeId));
      }
    };

    fetchData().then(() => setLoading(false));
  }, []);

  if (loading) return <Loading />;

  return (
    <AuthChecker>
      <BlankPage role={Status.STUDENT} pageTitle="Consulter une SAE">
        <Card variant="solid" color="primary" invertedColors>
          <CardContent orientation="horizontal">
            <CircularProgress
              size="lg"
              determinate
              value={(sae?.state + 1) * 20}
            >
              <DocumentScannerOutlined />
            </CircularProgress>
            <CardContent>
              <Typography level="body-md">IUT Blagnac</Typography>
              <Typography level="h2">{sae?.name}</Typography>
              <Typography level="body-md">
                {convertSaeStatutEnumToHText(
                  // @ts-ignore
                  convertSaeIntToStatutEnum(sae?.state)
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
        {team && (
          <>
            <Divider sx={{ mt: 2, mb: 2 }} />
            <Card>
              <Typography level="title-lg" sx={{ mb: 2 }}>
                Équipe
              </Typography>
              <Typography level="title-md">
                {team.nameTeam} | Team {team.colorTeam}
              </Typography>
              <Divider sx={{ mt: 2, mb: 2 }} />
              <Typography level="body-sm" sx={{ mb: 2 }}>
                {team.users.map((user) => (
                  <p key={user.id}>
                    {user.first_name} {user.last_name}
                  </p>
                ))}
              </Typography>
            </Card>
          </>
        )}
      </BlankPage>
    </AuthChecker>
  );
}
