import { Sheet, Table, Typography } from "@mui/joy";
import Topic from "../../../../models/Topic";
import { cutText } from "../../../../utils/Utils";

interface PendingWishesProps {
  topics: Topic[];
}

export default function PendingWishes({ topics }: PendingWishesProps) {
  return (
    <Sheet>
      <Typography level="h4">Sujets</Typography>
      <Table>
        <thead>
          <tr>
            <th>Intitulé</th>
            <th>Description</th>
            <th>Nombre de vœux</th>
          </tr>
        </thead>
        <tbody>
          {topics.map((topic) => (
            <tr key={topic.id}>
              <td>{topic.name}</td>
              <td>{cutText(topic.description, 25)}</td>
              <td>{2}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </Sheet>
  );
}
