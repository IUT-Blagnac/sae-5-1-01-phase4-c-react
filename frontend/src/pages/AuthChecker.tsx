import { useEffect, useState } from "react";
import React from "react";
import { CircularProgress } from "@mui/material";

interface AuthCheckerProps {
  children: React.ReactNode;
}

const AuthChecker: React.FC<AuthCheckerProps> = ({ children }) => {
  const [isUserAuthenticated, setIsUserAuthenticated] = useState<
    boolean | null
  >(null);

  useEffect(() => {
    const checkUser = async () => {
      try {
        const resUser = await fetch("/api/User/currentUser", {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        });

        if (resUser.status !== 200) {
          setIsUserAuthenticated(false);
        } else {
          setIsUserAuthenticated(true);
        }
      } catch (error) {
        console.error(
          "Erreur lors de la vérification de l'utilisateur :",
          error
        );
        setIsUserAuthenticated(false);
      }
    };

    checkUser();
  }, []);

  if (isUserAuthenticated === null) {
    <CircularProgress />;
    return null;
  }

  if (!isUserAuthenticated) {
    // Redirection si l'utilisateur n'est pas authentifié
    window.location.href = "/";
    return null; // Pour éviter le rendu du contenu de AuthChecker
  }

  return <>{children}</>;
};

export default AuthChecker;
