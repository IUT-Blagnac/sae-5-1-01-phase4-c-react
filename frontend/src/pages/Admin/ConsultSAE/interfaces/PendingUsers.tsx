import { Sheet, Table, Typography } from "@mui/joy";
import Topic from "../../../../models/Topic";
import { cutText, random } from "../../../../utils/Utils";

interface PendingUsersProps {
  topics: Topic[];
}

export default function PendingUsers({ topics }: PendingUsersProps) {
  return (
    <Sheet>
      <Typography level="h4">Sujets</Typography>
      <Table>
        <thead>
          <tr>
            <th>Intitulé</th>
            <th>Description</th>
          </tr>
        </thead>
        <tbody>
          {topics.map((topic) => (
            <tr key={topic.id}>
              <td>{topic.name}</td>
              <td>{cutText(topic.description, 25)}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </Sheet>
  );
}
