import Link from "@mui/material/Link";
import Typography from "@mui/material/Typography";

export default function Copyright(props: any) {
  return (
    <Typography variant="body2" color="text.secondary" {...props}>
      {"Copyright © "}
      <Link color="inherit" href="https://mui.com/">
        ProGest SAE Manager
      </Link>{" "}
      {new Date().getFullYear()}
      {"."}
    </Typography>
  );
}
