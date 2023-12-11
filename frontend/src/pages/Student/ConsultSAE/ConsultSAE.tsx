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
import {
  convertSaeStatutEnumToHText,
  convertSaeIntToStatutEnum,
} from "../../../utils/Utils";
import { DocumentScannerOutlined } from "@mui/icons-material";
import TopicServices from "../../../middlewares/Services/Topic.Services";
import SaeServices from "../../../middlewares/Services/Sae.Services";
import TeamServices, {
  User,
} from "../../../middlewares/Services/Team.Services";
import WishServices, {
  Wish,
} from "../../../middlewares/Services/Wish.Services";

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
  const [wish, setWish] = useState<Wish>();

  useEffect(() => {
    const saeId = window.location.href.split("/")[4];
    const userId = localStorage.getItem("userid") as string;

    const fetchData = async () => {
      const sae = await SaeServices.getSaeInfoFromUserId(userId, saeId);
      setSae(sae);
      setTopics(await TopicServices.getTopicsFromSae(saeId));

      if (sae?.state >= 1) {
        const team = await TeamServices.getTeamFromUserSae(userId, saeId);
        setTeam(team);

        const wish = await WishServices.getTeamWish(team.idTeam);
        setWish(wish);
      }
    };

    fetchData().then(() => setLoading(false));
  }, []);

  if (loading) return <Loading />;

  const makeWish = (topicId: string) => async () => {
    if (sae?.state !== 1) return;
    if (wish) return;
    const _wish = window.confirm(
      "Êtes-vous sûr de vouloir faire un vœu sur ce sujet ?"
    );

    if (_wish) {
      WishServices.postTeamWish(team?.idTeam as string, topicId);

      const topic = document.getElementById(`topic-${topicId}`) as HTMLElement;
      if (topic) {
        topic.style.border = "1px solid";
        topic.style.borderColor = "lightblue";
      }
    }
  };

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
        <Typography level="title-lg" sx={{ mb: 2 }}>
          Sujets
        </Typography>
        <Typography level="body-sm" sx={{ mb: 2 }}>
          ⚪ L'attribution des sujets n'est pas encore faite. <br />
          🔵 Le sujet entouré en bleu est le sujet sur lequel vous avez fait un
          vœu. <br />
          🟢 Si le sujet est entouré en vert c'est que vous avez été affecté
          dessus.
        </Typography>
        {topics.map((topic) => (
          <Card
            key={topic.id}
            onClick={makeWish(topic.id as string)}
            sx={{
              mt: 2,
              ":hover": {
                cursor: "pointer",
                border: "1px solid",
              },
              ...(wish?.id_subject === topic.id && {
                border: "1px solid",
                borderColor: "lightblue",
              }),
            }}
            id={`topic-${topic.id}`}
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
