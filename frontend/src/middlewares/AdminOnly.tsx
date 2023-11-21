import { useEffect, useState } from "react";
import React from "react";
import { CircularProgress } from "@mui/material";
import { URLs } from "../assets/enums/URLs.enum";
import { Status } from "../assets/enums/Status.enum";

interface AuthCheckerProps {
  children: React.ReactNode;
}

const AdminOnly: React.FC<AuthCheckerProps> = ({ children }) => {
  const [isUserAdmin, setIsUserAdmin] = useState<boolean | null>(null);

  useEffect(() => {
    const checkUser = async () => {
      const statut = localStorage.getItem("statut");
      setIsUserAdmin(statut === Status.ADMIN);
    };

    checkUser();
  }, []);

  if (isUserAdmin === null) {
    <CircularProgress />;
    return null;
  }

  if (!isUserAdmin) {
    window.location.href = URLs.DASHBOARD;
    return null;
  }

  return <>{children}</>;
};

export default AdminOnly;
