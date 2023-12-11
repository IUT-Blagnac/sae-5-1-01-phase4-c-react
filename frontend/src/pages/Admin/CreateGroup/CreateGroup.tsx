import React, { useEffect, useState } from "react";
import {
  Card,
  CardContent,
  Typography,
  Button,
  styled,
  FormControl,
  Input,
  FormHelperText,
  Checkbox,
  Select,
  Option,
  Box,
  Alert,
} from "@mui/joy";
import { Status } from "../../../assets/enums/Status.enum";
import BlankPage from "../../templates/BlankPage";
import { FormLabel } from "@mui/material";
import {
  CheckBox,
  GroupOutlined,
  PlaylistAddCheckCircleRounded,
} from "@mui/icons-material";
import GroupServices, {
  Group,
} from "../../../middlewares/Services/Group.Services";

const VisuallyHiddenInput = styled("input")`
  clip: rect(0 0 0 0);
  clip-path: inset(50%);
  height: 1px;
  overflow: hidden;
  position: absolute;
  bottom: 0;
  left: 0;
  white-space: nowrap;
  width: 1px;
`;

export default function CreateGroup() {
  const [groups, setGroups] = useState<Group[]>();
  const [groupName, setGroupName] = useState("");
  const [isAlternant, setIsAlternant] = useState(false);
  const [success, setSuccess] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      setGroups(await GroupServices.getGroups());
    };
    fetchData();
  }, []);

  const createGroup = async () => {
    if (groupName === "") return;

    setGroupName("");
    setIsAlternant(false);
    setSuccess(true);
    await GroupServices.createGroup(groupName, isAlternant);
    const groups = await GroupServices.getGroups();
    setGroups(groups);
  };

  return (
    <BlankPage
      pageTitle="Créer un groupe"
      role={Status.ADMIN}
      maxContentWidth="650px"
    >
      {success && (
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            gap: 2,
            width: "100%",
          }}
        >
          <Alert
            variant="soft"
            color="success"
            startDecorator={<PlaylistAddCheckCircleRounded />}
            endDecorator={
              <Button
                size="sm"
                variant="solid"
                color="success"
                onClick={() => setSuccess(false)}
              >
                Close
              </Button>
            }
          >
            Le groupe a bien été créé !
          </Alert>
        </Box>
      )}
      <Card>
        <CardContent>
          <Typography level="title-sm" component="div" gutterBottom>
            Consulter les groupes
          </Typography>
          <Select
            startDecorator={<GroupOutlined />}
            placeholder="Cliquez pour consulter les groupes"
          >
            {groups?.map((group) => (
              <Option value={group.id}>{group.name}</Option>
            ))}
          </Select>
        </CardContent>
      </Card>

      <Card>
        <CardContent>
          <Typography level="title-sm" component="div" gutterBottom>
            Créer un groupe
          </Typography>
          <Input
            placeholder="Nom du groupe"
            sx={{ mb: 2 }}
            onChange={(e) => setGroupName(e.target.value)}
            value={groupName}
          />

          <Checkbox
            label="Alternants"
            sx={{ mb: 4 }}
            onChange={(e) => setIsAlternant(e.target.checked)}
            checked={isAlternant}
          />
          <Button onClick={() => createGroup()}>Créer</Button>
        </CardContent>
      </Card>
    </BlankPage>
  );
}
