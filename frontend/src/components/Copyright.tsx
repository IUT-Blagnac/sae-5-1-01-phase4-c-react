import Link from "@mui/material/Link";
import Typography from "@mui/material/Typography";

interface CopyrightProps {
  sx: {
    mt: number;
  };
}

export default function Copyright(props: CopyrightProps) {
  return (
    <Typography
      variant="body2"
      color="text.secondary"
      align="center"
      {...props}
    >
      {"Copyright © "}
      <Link color="inherit" href="https://mui.com/">
        CiReact SAE Manager
      </Link>{" "}
      {new Date().getFullYear()}
      {"."}
    </Typography>
  );
}
