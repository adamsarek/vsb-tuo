package SAR0083.Homework.controllers;

import java.security.Principal;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PostMapping;

import SAR0083.Homework.StringToEmailAddressConverter;
import SAR0083.Homework.models.Account;
import SAR0083.Homework.services.AccountService;

@Controller
public class SecurityController {
	// Properties
	@Autowired
	private AccountService accountService;
	
	
	// Page: account
	@GetMapping(path = "/sign-up")
	public String signUp(Model model, Principal principal) {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", "Sign up");
		model.addAttribute("formButtonValue", "Sign up");
		model.addAttribute("account", new Account());
		
		return "signUp";
	}
	
	@PostMapping(path = "/sign-up/account")
	public String signUpAccount(@ModelAttribute Account account, Model model, Principal principal, HttpServletRequest request) throws Exception {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", "Sign up");
		model.addAttribute("formButtonValue", "Sign up");
		model.addAttribute("account", account);
		
		String username = account.getEmailAddress().toString();
		String password = account.getPassword();
		
		Account existingAccount = accountService.getAccount(account.getEmailAddress());
		
		if(existingAccount == null) {
			accountService.addAccount(account);
			
			try {
				request.login(username, password);
			} catch(ServletException e) {
				throw new Exception("Could not automatically sign in after signing up!");
			}
			
			return signInAccount(account, model, principal);
		}
		
		return "redirect:/sign-up";
	}
	
	@GetMapping(path = "/sign-in")
	public String signIn(Model model, Principal principal) {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", "Sign in");
		model.addAttribute("formButtonValue", "Sign in");
		model.addAttribute("account", new Account());
		
		return "signIn";
	}
	
	@PostMapping(path = "/sign-in/account")
	public String signInAccount(@ModelAttribute Account account, Model model, Principal principal) throws Exception {
		model.addAttribute("accountId", (principal != null ? accountService.getAccount(new StringToEmailAddressConverter().convert(principal.getName())).getId() : null));
		model.addAttribute("pageTitle", "Sign in");
		model.addAttribute("formButtonValue", "Sign in");
		model.addAttribute("account", account);
		
		Account existingAccount = accountService.getAccount(account.getEmailAddress());
		
		if(existingAccount != null
		&& existingAccount.getPassword().equals(account.getPassword())) {
			return "redirect:../";
		}
		
		return "redirect:/sign-in";
	}
}
